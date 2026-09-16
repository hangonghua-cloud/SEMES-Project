using ALP.WebApi.Filter;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;

namespace ALP.Application.WebApi.Controllers.API
{
    [Auth]    
    [RoutePrefix("api/demo")]
    public class DemoAPIController:ApiBaseController
    {
        [HttpGet]
        [Route("pp1")]
        public HttpResponseMessage PAPIDemo()
        {
            try
            {
                Result rst = new Result();
                rst.ResultData = "this is from POST PP1";
                rst.Success = true;
                rst.ReturnMsg = "this is from pp1 " + DateTime.Now.ToString();
                return Request.CreateResponse(HttpStatusCode.OK, rst);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result
                {
                    Success = false,
                    ReturnMsg = ex.Message,
                    ResultData = null
                });
            }
        }
        //[HttpPost]
        //[Route("processDemo")]
        //public HttpResponseMessage PrcessTest()
        //{
        //    try
        //    {
        //        Result rst = new Result();
        //        转为json对象
        //        get data by the key
        //        List<Person> list = new List<Person>();
        //        list.Add(new Person(43, "1409", 57));
        //        list.Add(new Person(56, "TB01", 44));
        //        list.Add(new Person(1, "延伸复合", 99));
        //        list.Add(new Person(88, "放卷", 12));
        //        list.Add(new Person(32, "投料", 68));
        //        string strJson = MES.Code.Json.ObjToJson<List<Person>>(list);

        //        rst.ResultData = list;
        //        WriteLog(" rst.ResultData:   ");
        //        rst.Success = true;
        //        rst.ReturnMsg = "this is from PP2 " + DateTime.Now.ToString();


        //        return Request.CreateResponse(HttpStatusCode.OK, rst);
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteLog("ex.Message:   " + ex.Message);
        //        return Request.CreateResponse(HttpStatusCode.OK, new Result
        //        {
        //            Success = false,
        //            ReturnMsg = ex.Message,
        //            ResultData = null
        //        });
        //    }
        //}


        //[HttpPost]
        //[Route("GetProcessCapacity")]
        //public HttpResponseMessage GetProcessCapacity(JObject json)
        //{
        //    Result rst = new Result();
        //    try
        //    {
        //        if (json != null)
        //        {
        //            var jsonText = json.ToString();
        //            JObject jo = (JObject)JsonConvert.DeserializeObject(jsonText);
        //            string jsonData = jo["json"].ToString();
        //            var jsonDataObject = (JObject)JsonConvert.DeserializeObject(jsonData);
        //            var ProductionLine = jsonDataObject["ProductionLine"].ToString();
        //            var Op = jsonDataObject["Op"].ToString();

        //        }

        //        ProduceCapa list = new ProduceCapa()
        //        {
        //            ListMaxValue = 200,
        //            ListTotoalMaxValue = 1.5,
        //            LineListA = new List<LineList>()
        //               {
        //                   new LineList() { X = "1", Y = "1.1" },
        //                   new LineList() { X = "2", Y = "1.2" },
        //                   new LineList() { X = "3", Y = "1.1" },
        //                   new LineList() { X = "4", Y = "1.3" },
        //                   new LineList() { X = "5", Y = "1.4" },
        //                   new LineList() { X = "6", Y = "1.5" },
        //                   new LineList() { X = "7", Y = "1" },
        //                   new LineList() { X = "8", Y = "1.4" },
        //                   new LineList() { X = "9", Y = "1.2" },
        //                   new LineList() { X = "10", Y = "1.1" },
        //                   new LineList() { X = "11", Y = "1.3" },
        //                   new LineList() { X = "12", Y = "1.1" },
        //                   new LineList() { X = "13", Y = "1.5" },
        //                   new LineList() { X = "14", Y = "1.4" },
        //                   new LineList() { X = "15", Y = "1.1" },
        //                   new LineList() { X = "16", Y = "1.3" },
        //                   new LineList() { X = "17", Y = "1.4" },
        //                   new LineList() { X = "18", Y = "1.2" },
        //                   new LineList() { X = "19", Y = "1.5" },
        //                   new LineList() { X = "20", Y = "1" },
        //                   new LineList() { X = "21", Y = "0" },
        //                   new LineList() { X = "22", Y = "0" },
        //                   new LineList() { X = "23", Y = "0" },
        //                   new LineList() { X = "24", Y = "0"},
        //                   new LineList() { X = "25", Y = "0" },
        //                   new LineList() { X = "26", Y = "0" },
        //                   new LineList() { X = "27", Y = "0" },
        //                   new LineList() { X = "28", Y = "0" },
        //                   new LineList() { X = "29", Y = "0" },
        //                   new LineList() { X = "30", Y = "0" },
        //                   new LineList() { X = "31", Y = "0" }
        //               },
        //            LineListB = new List<LineList>()
        //               {
        //                   new LineList() { X = "1", Y = "1.2" },
        //                   new LineList() { X = "2", Y = "1.5" },
        //                   new LineList() { X = "3", Y = "1.1" },
        //                   new LineList() { X = "4", Y = "1.3" },
        //                   new LineList() { X = "5", Y = "1.2" },
        //                   new LineList() { X = "6", Y = "1.4" },
        //                   new LineList() { X = "7", Y = "1.1" },
        //                   new LineList() { X = "8", Y = "1.4" },
        //                   new LineList() { X = "9", Y = "1.3" },
        //                   new LineList() { X = "10", Y = "1" },
        //                   new LineList() { X = "11", Y = "1.1" },
        //                   new LineList() { X = "12", Y = "1.4" },
        //                   new LineList() { X = "13", Y = "1.2" },
        //                   new LineList() { X = "14", Y = "1.4" },
        //                   new LineList() { X = "15", Y = "1.1" },
        //                   new LineList() { X = "16", Y = "1.3" },
        //                   new LineList() { X = "17", Y = "1.1" },
        //                   new LineList() { X = "18", Y = "1.4" },
        //                   new LineList() { X = "19", Y = "1.1" },
        //                   new LineList() { X = "20", Y = "1.4" },
        //                   new LineList() { X = "21", Y = "0" },
        //                   new LineList() { X = "22", Y = "0" },
        //                   new LineList() { X = "23", Y = "0" },
        //                   new LineList() { X = "44", Y = "0" },
        //                   new LineList() { X = "25", Y = "0" },
        //                   new LineList() { X = "26", Y = "0" },
        //                   new LineList() { X = "27", Y = "0" },
        //                   new LineList() { X = "28", Y = "0" },
        //                   new LineList() { X = "29", Y = "0" },
        //                   new LineList() { X = "30", Y = "0" },
        //                   new LineList() { X = "31", Y = "0" }
        //               },
        //            LineListTotoalA = new List<LineList>()
        //               {
        //                   new LineList() { X = "1", Y = "10" },
        //                   new LineList() { X = "2", Y = "20" },
        //                   new LineList() { X = "3", Y = "30" },
        //                   new LineList() { X = "4", Y = "40" },
        //                   new LineList() { X = "5", Y = "50" },
        //                   new LineList() { X = "6", Y = "60" },
        //                   new LineList() { X = "7", Y = "70" },
        //                   new LineList() { X = "8", Y = "80" },
        //                   new LineList() { X = "9", Y = "90" },
        //                   new LineList() { X = "10", Y = "100" },
        //                   new LineList() { X = "11", Y = "110" },
        //                   new LineList() { X = "12", Y = "120" },
        //                   new LineList() { X = "13", Y = "130" },
        //                   new LineList() { X = "14", Y = "140" },
        //                   new LineList() { X = "15", Y = "150" },
        //                   new LineList() { X = "16", Y = "160" },
        //                   new LineList() { X = "17", Y = "170" },
        //                   new LineList() { X = "18", Y = "180" },
        //                   new LineList() { X = "19", Y = "190" },
        //                   new LineList() { X = "20", Y = "200" },
        //                   new LineList() { X = "21", Y = "0" },
        //                   new LineList() { X = "22", Y = "0" },
        //                   new LineList() { X = "23", Y = "0" },
        //                   new LineList() { X = "44", Y = "0" },
        //                   new LineList() { X = "25", Y = "0" },
        //                   new LineList() { X = "26", Y = "0" },
        //                   new LineList() { X = "27", Y = "0" },
        //                   new LineList() { X = "28", Y = "0" },
        //                   new LineList() { X = "29", Y = "0" },
        //                   new LineList() { X = "30", Y = "0" },
        //                   new LineList() { X = "31", Y = "0" }
        //               },
        //            LineListTotoalB = new List<LineList>()
        //               {
        //                   new LineList() { X = "1", Y = "10" },
        //                   new LineList() { X = "2", Y = "20" },
        //                   new LineList() { X = "3", Y = "30" },
        //                   new LineList() { X = "4", Y = "40" },
        //                   new LineList() { X = "5", Y = "50" },
        //                   new LineList() { X = "6", Y = "60" },
        //                   new LineList() { X = "7", Y = "70" },
        //                   new LineList() { X = "8", Y = "80" },
        //                   new LineList() { X = "9", Y = "90" },
        //                   new LineList() { X = "10", Y = "100" },
        //                   new LineList() { X = "11", Y = "110" },
        //                   new LineList() { X = "12", Y = "120" },
        //                   new LineList() { X = "13", Y = "130" },
        //                   new LineList() { X = "14", Y = "140" },
        //                   new LineList() { X = "15", Y = "150" },
        //                   new LineList() { X = "16", Y = "160" },
        //                   new LineList() { X = "17", Y = "170" },
        //                   new LineList() { X = "18", Y = "180" },
        //                   new LineList() { X = "19", Y = "190" },
        //                   new LineList() { X = "20", Y = "200" },
        //                   new LineList() { X = "21", Y = "0" },
        //                   new LineList() { X = "22", Y = "0" },
        //                   new LineList() { X = "23", Y = "0" },
        //                   new LineList() { X = "44", Y = "0" },
        //                   new LineList() { X = "25", Y = "0" },
        //                   new LineList() { X = "26", Y = "0" },
        //                   new LineList() { X = "27", Y = "0" },
        //                   new LineList() { X = "28", Y = "0" },
        //                   new LineList() { X = "29", Y = "0" },
        //                   new LineList() { X = "30", Y = "0" },
        //                   new LineList() { X = "31", Y = "0" }
        //               }
        //        };
        //        rst.ResultData = list;
        //        rst.Success = true;
        //        rst.ReturnMsg = "执行成功";

        //        return Request.CreateResponse(HttpStatusCode.OK, rst);

        //    }
        //    catch (Exception ex)
        //    {
        //        WriteLog("ex.Message:   " + ex.Message);
        //        return Request.CreateResponse(HttpStatusCode.OK, new Result
        //        {
        //            Success = false,
        //            ReturnMsg = ex.Message,
        //            ResultData = null
        //        });
        //    }
        //}
        ///// <summary>
        ///// APP post json with json object such as :  {"j":1111}
        ///// return List object
        ///// </summary>
        ///// <param name="json"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("pp2")]
        //public HttpResponseMessage PAPIDemo2()
        //{
        //    try
        //    {
        //        Result rst = new Result();
        //        //转为json对象
        //        //get data by the key
        //        List<TempText> list = new List<TempText>();
        //        list.Add(new TempText() { DeviceName = "1330线上TAC放卷", DeviceID = 17, TempValue = "21.6", DampValue = "41.3" });
        //        list.Add(new TempText() { DeviceName = "1330线上TAC放卷", DeviceID = 18, TempValue = "23.7", DampValue = "49.3" });
        //        list.Add(new TempText() { DeviceName = "1330线上TAC放卷", DeviceID = 20, TempValue = "23.6", DampValue = "46.3" });
        //        list.Add(new TempText() { DeviceName = "1330线上TAC放卷", DeviceID = 5, TempValue = "21.3", DampValue = "42" });
        //        list.Add(new TempText() { DeviceName = "1330线上TAC放卷", DeviceID = 6, TempValue = "21.3", DampValue = "41.6" });
        //        list.Add(new TempText() { DeviceName = "1330线上TAC放卷", DeviceID = 7, TempValue = "23.6", DampValue = "44.3" });

        //        //string strJson = MES.Code.Json.ObjToJson<List<Person>>(list);

        //        rst.ResultData = list;
        //        WriteLog(" rst.ResultData:   ");
        //        rst.Success = true;
        //        rst.ReturnMsg = "this is from PP2 " + DateTime.Now.ToString();


        //        return Request.CreateResponse(HttpStatusCode.OK, rst);
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteLog("ex.Message:   " + ex.Message);
        //        return Request.CreateResponse(HttpStatusCode.OK, new Result
        //        {
        //            Success = false,
        //            ReturnMsg = ex.Message,
        //            ResultData = null
        //        });
        //    }
        //}



        ///// <summary>
        ///// APP post json with json array object such as :  {"j":1111}
        ///// get data from input data which posted by Android 
        ///// </summary>
        ///// <param name="json"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("pp3")]
        //public HttpResponseMessage PAPIDemo3(JObject json)
        //{
        //    try
        //    {
        //        Result rst = new Result();
        //        //转为json对象
        //        if (json != null)
        //        {
        //            var jsonText = json.ToString();
        //            JObject jo = (JObject)JsonConvert.DeserializeObject(jsonText);
        //            string jsonData = jo["json"].ToString();

        //            JObject JsonDataToJobject = (JObject)JsonConvert.DeserializeObject(jsonData);
        //            var array = JsonDataToJobject["array"];
        //            List<string> list = new List<string>();
        //            foreach (var jobject in array)
        //            {
        //                JObject jsonobject = (JObject)JsonConvert.DeserializeObject(jobject.ToString());
        //                var name = jobject["name"].ToString();
        //               // CheckLogEntity enity = new CheckLogEntity();
        //               // enity.EquipmentCode = name;

        //                list.Add(jsonobject["j"].ToString());
        //            }

        //            //WriteLog.RecordLog(JsonDataToJobject["j"].ToString());
        //            //get data by the key
        //            rst.ResultData = list;
        //            rst.Success = true;
        //            rst.ReturnMsg = "this is from PP3 " + DateTime.Now.ToString();
        //        }
        //        // var data = JsonConvert.SerializeObject(rst);
        //        return Request.CreateResponse(HttpStatusCode.OK, rst);
        //    }
        //    catch (Exception ex)
        //    {

        //        return Request.CreateResponse(HttpStatusCode.OK, new Result
        //        {
        //            Success = false,
        //            ReturnMsg = ex.Message,
        //            ResultData = null
        //        });
        //    }
        //}

        ///// <summary>
        ///// APP post json with json object 
        ///// return one object
        ///// </summary>
        ///// <param name="json"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("pp5")]
        //public HttpResponseMessage PAPIDemo5(JObject json)
        //{
        //    try
        //    {
        //        Result rst = new Result();
        //        //转为json对象
        //        if (json != null)
        //        {
        //            var jsonText = json.ToString();
        //            JObject jo = (JObject)JsonConvert.DeserializeObject(jsonText);
        //            string jsonData = jo["json"].ToString();
        //            JObject JsonDataToJobject = (JObject)JsonConvert.DeserializeObject(jsonData);
        //            //get data by the key


        //            rst.ResultData = new Person(3, "刘备", 23);
        //            WriteLog(" rst.ResultData:   ");
        //            rst.Success = true;
        //            rst.ReturnMsg = "this is from PP5 " + DateTime.Now.ToString();
        //        }
        //        return Request.CreateResponse(HttpStatusCode.OK, rst);
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteLog("ex.Message:   " + ex.Message);
        //        return Request.CreateResponse(HttpStatusCode.OK, new Result
        //        {
        //            Success = false,
        //            ReturnMsg = ex.Message,
        //            ResultData = null
        //        });
        //    }
        //}

    }
    public class Result
    {
        public bool Success { get; set; }
        public object ResultData { get; set; }
        public string ReturnMsg { get; set; }
    }
    public class TempText
    {
        public int DeviceID { get; set; }
        public string DeviceName { get; set; }
        public string TempValue { get; set; }
        public string DampValue { get; set; }
    }
    public class ProduceCapa
    {
        public dynamic ListMaxValue { get; set; }
        public dynamic ListTotoalMaxValue { get; set; }

        public List<LineList> LineListA;
        public List<LineList> LineListB;
        public List<LineList> LineListTotoalA;
        public List<LineList> LineListTotoalB;

    }

    #region demo entity
    public class LineList
    {
        public string X { get; set; }
        public string Y { get; set; }
    }
    public class Person
    {
        public Person(int id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }

        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public int Age
        {
            get;
            set;
        }
    }
    public class Test
    {
        public string j
        {
            get;
            set;
        }
    }
    #endregion
}
