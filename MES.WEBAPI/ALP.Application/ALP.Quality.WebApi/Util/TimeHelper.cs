using System;
using System.Data;
using ALP.Data;
using ALP.Data.Repository;

namespace ALP.Application.WebApi.Util
{
    /// <summary>
    /// 定时任务操作
    /// </summary>
    public  class TimeHelper
    {
        /// <summary>
        /// 启动定时任务
        /// </summary>
        public static void Start()
        {
            try
            {
                System.Timers.Timer timer = new System.Timers.Timer();
                
                timer.Enabled = true;
                timer.Interval = 300000;
                timer.Elapsed += new System.Timers.ElapsedEventHandler(TimeOperation);
                timer.AutoReset = true;
                timer.Start();
                //Thread.Sleep(35000);
                //timer.Stop();

            }
            catch(Exception ex)
            {
            }         

        }

        public static DateTimeOffset dateTimeOpMark = DateTimeOffset.Now.AddDays(-1);
                              
        /// <summary>
        /// 定时操作内容：根据设备点检规则定时生成设备点检计划
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void TimeOperation(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                DateTimeOffset newDateTime = DateTimeOffset.Now;

                //if (newDateTime < dateTimeOpMark.AddDays(1))
                //    return;

                dateTimeOpMark = newDateTime;

                IDatabase _db = DbFactory.Base();

                //string sql = " update Mes_Qulity_InspectionRecord set InspectionType='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";

                //获取设备点检规则数据
                string sqlRuleData = " select e.Code,e.[Name],r.EquipmentType,e.LineCode,r.[Period],r.PeriodUom,r.Note,r.IsEnabled,d.MaintenanceItemCode,i.[Name] as MaintenanceItemName, d.Note as DetailNote,d.IsEnabled as DetailIsEnabled from Mes_Equip_Equipment as e left join Mes_Equip_MaintenanceRule as r on r.EquipmentType=e.Type left join Mes_Equip_MaintenanceRuleItem as d on r.Id=d.MaintenanceRuleId LEFT JOIN Mes_Equip_MaintenanceItem i ON d.MaintenanceItemCode=i.Code where r.IsEnabled=1  and d.IsEnabled=1 and e.Status='1' order by e.Code";

                DataTable dtRuleData = _db.FindTable(sqlRuleData);

                string equipCode = "";
                string equipCodeMark = "";

                string sqlPlan = "";
                DataTable dtPlan;
                //定时生成保养计划    //生成保养计划，当前周期在第一天，需要生成下一个周期在保养计划

                for (int i = 0; i < dtRuleData.Rows.Count; i++)
                {
                    equipCode = dtRuleData.Rows[i]["Code"].ToString();
                    
                    if (string.IsNullOrEmpty(equipCode))
                        continue;

                    if (equipCode != equipCodeMark)
                    {

                        //获取最新的保养计划                      
                        sqlPlan = $"  select top 1 * from Mes_Equip_MaintainPlan where EquipCode='{equipCode}' order by PlanStartDate desc ";
                        dtPlan = null;
                        _db = DbFactory.Base();
                        dtPlan = _db.FindTable(sqlPlan);
                        if (dtPlan == null || dtPlan.Rows.Count < 1)
                        {
                            if (!CreatePlanFromNull(dtRuleData, equipCode))
                                CreatePlanFromNull(dtRuleData, equipCode);
                        }
                        else
                        {
                            DateTimeOffset dtt =(DateTimeOffset) dtPlan.Rows[0]["PlanFinishDate"];


                            if (!CreatePlanFromNow(dtRuleData, equipCode, ToDatetime(((DateTimeOffset)dtPlan.Rows[0]["PlanFinishDate"]).ToString("yyyy-MM-dd")), ToDatetime(((DateTimeOffset)dtPlan.Rows[0]["PlanStartDate"]).ToString("yyyy-MM-dd"))))
                                CreatePlanFromNow(dtRuleData, equipCode, ToDatetime(((DateTimeOffset)dtPlan.Rows[0]["PlanFinishDate"]).ToString("yyyy-MM-dd")), ToDatetime(((DateTimeOffset)dtPlan.Rows[0]["PlanStartDate"]).ToString("yyyy-MM-dd")));
                        }
                    }
                    else
                    {
                        continue;

                    }

                    equipCodeMark = equipCode;        
                }


            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

       /// <summary>
       /// 获取保养结束时间
       /// </summary>
       /// <param name="dtMark"></param>
       /// <param name="unitMark"></param>
       /// <param name="periodMark"></param>
       /// <returns></returns>
        public static DateTimeOffset GetDays(DateTimeOffset dtMark,string unitMark,int periodMark)
        {
            DateTimeOffset result = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));

            try
            {
                if (dtMark == null)
                    dtMark = result;


                dtMark = Convert.ToDateTime(dtMark.ToString("yyyy-MM-dd"));

                switch (unitMark.ToLower()) {
                    case "日": result = dtMark.AddDays(periodMark); break;
                    case "周": result = dtMark.AddDays(periodMark*7); break;
                    case "月": result = dtMark.AddMonths(periodMark); break;
                    case "年": result = dtMark.AddYears(periodMark); break;
                    case "day": result = dtMark.AddDays(periodMark); break;
                    case "week": result = dtMark.AddDays(periodMark * 7); break;
                    case "month": result = dtMark.AddMonths(periodMark); break;
                    case "year": result = dtMark.AddYears(periodMark); break;


                }
            }
            catch(Exception ex)
            {

            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtRuleData"></param>
        /// <param name="equipCode"></param>
        /// <param name="lastFinishTime"></param>
        /// <param name="lastStartTime"></param>
        /// <returns></returns>
        public static bool CreatePlanFromNow(DataTable dtRuleData,string equipCode,DateTimeOffset lastFinishTime, DateTimeOffset lastStartTime)
        {
            bool result = true;
            try
            {
                if (lastStartTime > Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")))
                    return result;


                if(lastFinishTime==null||lastFinishTime< Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")))
                {
                    CreatePlanFromNull(dtRuleData, equipCode);
                    return result;
                }
                string equipCodeMark = "";

                IDatabase _db = DbFactory.Base();

                DateTimeOffset planFinishTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                DateTimeOffset nextPlanFinishTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                Guid mainKeyValue;
                Guid nextMainKeyValue;
                string sqlInsertItem = "";
                string sqlInsertDetail = "";
                nextMainKeyValue = Guid.NewGuid();
                int opMark = 0;
                planFinishTime = lastFinishTime;
               

                for (int i = 0; i < dtRuleData.Rows.Count; i++)
                {
                    equipCodeMark = dtRuleData.Rows[i]["Code"].ToString();

                    if (string.IsNullOrEmpty(equipCodeMark))
                        continue;

                    if (equipCode != equipCodeMark)
                        continue;

                
                    //下个周期保养计划

                    if (opMark == 0)
                    {
                        nextPlanFinishTime = GetDays(planFinishTime, ToString(dtRuleData.Rows[i]["PeriodUom"]), ToInt(dtRuleData.Rows[i]["Period"]));
                        sqlInsertItem = $"  insert into Mes_Equip_MaintainPlan (Id,EquipType,EquipCode,EquipName,WorkShop,PeriodValue,PeriodUnit,PlanStartDate,PlanFinishDate,Note,IsClosed,CreatedOn,CreatedByCode,CreatedByName) values ('{nextMainKeyValue}','{ToString(dtRuleData.Rows[i]["EquipmentType"])}','{ToString(dtRuleData.Rows[i]["Code"])}','{ToString(dtRuleData.Rows[i]["Name"])}','{ToString(dtRuleData.Rows[i]["LineCode"])}',{ToInt(dtRuleData.Rows[i]["Period"])},'{ToString(dtRuleData.Rows[i]["PeriodUom"])}','{planFinishTime}','{nextPlanFinishTime}','{ToString(dtRuleData.Rows[i]["Note"])}',0,'{DateTimeOffset.Now}','testAdmin','定时生成')";

                        _db.ExecuteBySql(sqlInsertItem);
                    }

                    sqlInsertDetail = $"  insert into Mes_Equip_MaintainPlanItem (MaintainPlanId,MaintenanceItemCode,MaintenanceItemName,Note,CreatedOn,CreatedByCode,CreatedByName) values ('{nextMainKeyValue}','{ToString(dtRuleData.Rows[i]["MaintenanceItemCode"])}','{ToString(dtRuleData.Rows[i]["MaintenanceItemName"])}','{ToString(dtRuleData.Rows[i]["DetailNote"])}','{DateTimeOffset.Now}','testAdmin','定时生成')";

                    _db.ExecuteBySql(sqlInsertDetail);

                    opMark++;

                }




            }
            catch(Exception ex)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 在没有保养计划时创造保养计划
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="equipCode"></param>
        /// <returns></returns>
        public static bool CreatePlanFromNull(DataTable dtRuleData, string equipCode)
        {
            bool result = true;
            string equipCodeMark = "";
            
            try
            {
                IDatabase _db = DbFactory.Base();

                DateTimeOffset planStartTime = ToDatetime(DateTimeOffset.Now.ToString("yyyy-MM-dd"));

                DateTimeOffset planFinishTime = ToDatetime(DateTimeOffset.Now.ToString("yyyy-MM-dd"));
                DateTimeOffset nextPlanFinishTime = ToDatetime(DateTimeOffset.Now.ToString("yyyy-MM-dd"));
                Guid mainKeyValue;
                Guid nextMainKeyValue;
                string sqlInsertItem = "";
                string sqlInsertDetail = "";
                mainKeyValue = Guid.NewGuid();
                nextMainKeyValue = Guid.NewGuid();
                int opMark = 0;              
                

                for (int i = 0; i < dtRuleData.Rows.Count; i++)
                {
                    equipCodeMark = dtRuleData.Rows[i]["Code"].ToString();

                    if (string.IsNullOrEmpty(equipCodeMark))
                        continue;

                    if (equipCode != equipCodeMark)
                        continue;

                    //当前周期保养计划
                    if (opMark == 0)
                    {
                        planFinishTime = GetDays(DateTimeOffset.Now, ToString(dtRuleData.Rows[i]["PeriodUom"]), ToInt(dtRuleData.Rows[i]["Period"]));
                        sqlInsertItem = $"  insert into Mes_Equip_MaintainPlan (Id,EquipType,EquipCode,EquipName,WorkShop,PeriodValue,PeriodUnit,PlanStartDate,PlanFinishDate,Note,IsClosed,CreatedOn,CreatedByCode,CreatedByName) values ('{mainKeyValue}','{ToString(dtRuleData.Rows[i]["EquipmentType"])}','{ToString(dtRuleData.Rows[i]["Code"])}','{ToString(dtRuleData.Rows[i]["Name"])}','{ToString(dtRuleData.Rows[i]["LineCode"])}',{ToInt(dtRuleData.Rows[i]["Period"])},'{ToString(dtRuleData.Rows[i]["PeriodUom"])}','{ToDatetime(DateTimeOffset.Now.ToString("yyyy-MM-dd"))}','{planFinishTime}','{ToString(dtRuleData.Rows[i]["Note"])}',0,'{DateTimeOffset.Now}','testAdmin','定时生成')";

                        _db.ExecuteBySql(sqlInsertItem);
                    }
                   

                    sqlInsertDetail = $"  insert into Mes_Equip_MaintainPlanItem (MaintainPlanId,MaintenanceItemCode,MaintenanceItemName,Note,CreatedOn,CreatedByCode,CreatedByName) values ('{mainKeyValue}','{ToString(dtRuleData.Rows[i]["MaintenanceItemCode"])}','{ToString(dtRuleData.Rows[i]["MaintenanceItemName"])}','{ToString(dtRuleData.Rows[i]["DetailNote"])}','{DateTimeOffset.Now}','testAdmin','定时生成')";

                    _db.ExecuteBySql(sqlInsertDetail);

                    //下个周期保养计划

                    if (opMark == 0)
                    {
                        nextPlanFinishTime = GetDays(planFinishTime, ToString(dtRuleData.Rows[i]["PeriodUom"]), ToInt(dtRuleData.Rows[i]["Period"]));
                        sqlInsertItem = $"  insert into Mes_Equip_MaintainPlan (Id,EquipType,EquipCode,EquipName,WorkShop,PeriodValue,PeriodUnit,PlanStartDate,PlanFinishDate,Note,IsClosed,CreatedOn,CreatedByCode,CreatedByName) values ('{nextMainKeyValue}','{ToString(dtRuleData.Rows[i]["EquipmentType"])}','{ToString(dtRuleData.Rows[i]["Code"])}','{ToString(dtRuleData.Rows[i]["Name"])}','{ToString(dtRuleData.Rows[i]["LineCode"])}',{ToInt(dtRuleData.Rows[i]["Period"])},'{ToString(dtRuleData.Rows[i]["PeriodUom"])}','{planFinishTime}','{nextPlanFinishTime}','{ToString(dtRuleData.Rows[i]["Note"])}',0,'{DateTimeOffset.Now}','testAdmin','定时生成')";

                        _db.ExecuteBySql(sqlInsertItem);
                    }

                   

                    sqlInsertDetail = $"  insert into Mes_Equip_MaintainPlanItem (MaintainPlanId,MaintenanceItemCode,MaintenanceItemName,Note,CreatedOn,CreatedByCode,CreatedByName) values ('{nextMainKeyValue}','{ToString(dtRuleData.Rows[i]["MaintenanceItemCode"])}','{ToString(dtRuleData.Rows[i]["MaintenanceItemName"])}','{ToString(dtRuleData.Rows[i]["DetailNote"])}','{DateTimeOffset.Now}','testAdmin','定时生成')";

                    _db.ExecuteBySql(sqlInsertDetail);

                    opMark++;

                }
            }
            catch(Exception ex)
            {
                result = false;
            }
            return result;
        }


        /// <summary>
        ///  转换为字符串
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string ToString(object o)
        {
            string result = "";
            try
            {
                if (o is DBNull || o == null)
                    result = "";
                else
                    result = Convert.ToString(o);
            }
            catch (Exception)
            {
                return "";
            }

            return result;
        }

        /// <summary>
        ///  转换为decimal
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static decimal ToDecimal(object o)
        {
            decimal result = 0;
            try
            {
                if (o is DBNull || o == null)
                    result = 0;
                else
                    result = Convert.ToDecimal(o);
            }
            catch (Exception)
            {
                return 0;
            }

            return result;
        }

        /// <summary>
        ///  转换为int
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static int ToInt(object o)
        {
            int result = 0;
            try
            {
                if (o is DBNull || o == null)
                    result = 0;
                else
                    result = Convert.ToInt32(o);
            }
            catch (Exception)
            {
                return 0;
            }

            return result;
        }


        /// <summary>
        ///  转换为bool
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool ToBool(object o)
        {
            bool result = false;
            if (o is DBNull || o == null)
                result = false;
            else
                result = Convert.ToBoolean(o);

            return result;
        }

        public static DateTimeOffset ToDatetime(object o)
        {
            DateTimeOffset timeResult = Convert.ToDateTime( DateTime.Now.ToString("yyyy-MM-dd"));
            try
            {
                if (o is DBNull || o == null)
                    timeResult = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                else
                    timeResult = Convert.ToDateTime(Convert.ToDateTime(o).ToString("yyyy-MM-dd"));
            }
            catch(Exception ex)
            {
                timeResult = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            }

            return timeResult;
        }


    }
}
