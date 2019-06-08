using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Web.Script.Serialization;

namespace TestApp.Common
{
    public static class ParseHelpers
    {
        public static void Test()
        {
            var fileNamePath = System.Environment.CurrentDirectory + @"\Helper\StepList.xml";
            var list = DeserializeXmlFileToObject<StepList>(fileNamePath);

            fileNamePath = System.Environment.CurrentDirectory + @"\Helper\StepList2.xml";
            SerializeXmlFileToObject<StepList>(fileNamePath, list);
        }

        public static void Test2()
        {
            var fileNamePath = System.Environment.CurrentDirectory + @"\Helper\StepList.xml";
            string xml = File.ReadAllText(fileNamePath);
            var catalog1 = xml.ParseXml2Obj<StepList>();
        }

        /// <summary>
        /// 对象保存到xml文件,对象需要XmlElement描述
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlFilename"></param>
        /// <param name="obj"></param>
        public static void SerializeXmlFileToObject<T>(string xmlFilename, T obj)
        {
            if (string.IsNullOrEmpty(xmlFilename))
            {
                return;
            }

            try
            {
                StreamWriter xmlStream = new StreamWriter(xmlFilename);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(xmlStream, obj);
            }
            catch (Exception)
            {
                //ExceptionLogger.WriteExceptionToConsole(ex, DateTime.Now);
            }
        }

        /// <summary>
        /// 解析xml文件成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlFilename"></param>
        /// <returns></returns>
        public static T DeserializeXmlFileToObject<T>(string xmlFilename)
        {
            T returnObject = default(T);
            if (string.IsNullOrEmpty(xmlFilename))
            {
                return default(T);
            }
            try
            {
                StreamReader xmlStream = new StreamReader(xmlFilename);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                returnObject = (T)serializer.Deserialize(xmlStream);
            }
            catch (Exception)
            {
                //ExceptionLogger.WriteExceptionToConsole(ex, DateTime.Now);
            }
            return returnObject;
        }

        public static Stream ToStream(this string @this)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(@this);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static T ParseXml2Obj<T>(this string @this) where T : class
        {
            var reader = XmlReader.Create(@this.Trim().ToStream(), new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Document });
            return new XmlSerializer(typeof(T)).Deserialize(reader) as T;
        }

        //private static JavaScriptSerializer json;
        //private static JavaScriptSerializer Json { get { return json ?? (json = new JavaScriptSerializer()); } }
        //public static T ParseJson2Obj<T>(this string @this) where T : class
        //{
        //    return Json.Deserialize<T>(@this.Trim());
        //}

        public static T ParseXml2Obj2<T>(string xml) where T : class
        {
            var ser = new XmlSerializer(typeof(T));
            return (T)ser.Deserialize(new StringReader(xml));
        }

        public static T ParseXml2Obj3<T>(string xmlFilename) where T : class
        {
            var ser = new XmlSerializer(typeof(T));
            using (var reader = XmlReader.Create(xmlFilename))
            {
                return (T)ser.Deserialize(reader);
            }
        }

        public static T ParseXml2Obj4<T>(string xmlFilename) where T : class
        {
            XmlDocument _Doc = new XmlDocument();
            _Doc.Load(xmlFilename);
            var ser = new XmlSerializer(typeof(T));
            return (T)ser.Deserialize(new StringReader(_Doc.OuterXml));
        }

        public static T ParseXml2Obj5<T>(string xmlFilename) where T : class
        {
            XmlDocument _Doc = new XmlDocument();
            _Doc.Load(xmlFilename);
            var ser = new XmlSerializer(typeof(T));
            return (T)ser.Deserialize(new XmlNodeReader(_Doc.DocumentElement));
        }
    }

    public class StepList
    {
        [XmlElement("Step")]
        public List<Step> Steps { get; set; }
    }

    public class Step
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Desc")]
        public string Desc { get; set; }

        [XmlElement("List1")]
        public string List1 { get; set; }

        [XmlElement("List2")]
        public List<string> List2 { get; set; }
    }
}