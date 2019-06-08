using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TestApp.Common
{
    public class XMLHelper2
    {
        public static XmlDocument CreateCommonXml()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", "");
            xmlDoc.AppendChild(xmlDeclaration);
            return xmlDoc;
        }

        public static XmlDocument CreateCommonXmlWithRoot()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", "");
            xmlDoc.AppendChild(xmlDeclaration);
            XmlNode root = xmlDoc.CreateElement("root");
            xmlDoc.AppendChild(root);
            return xmlDoc;
        }

        public static bool CheckXml(string xml)
        {
            return !string.IsNullOrEmpty(xml) && xml.StartsWith("<?xml");
        }

        public static XmlDocument LoadXml(string xml)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(xml);
            return xDoc;
        }

        public static bool LoadXml(string xml, ref XmlDocument xDoc)
        {
            try
            {
                if (!string.IsNullOrEmpty(xml) && xml.StartsWith("<?xml"))
                {
                    xDoc.LoadXml(xml);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public static string SelectSingleNodeInnerText(XmlDocument xDoc, string xpath)
        {
            XmlNode node = xDoc.SelectSingleNode(xpath);
            return node != null ? node.InnerText : null;
        }

        public static XmlNode CreateOrUpdateXmlNodeByXPath(XmlDocument xDoc, string xpath)
        {
            string[] nodes = xpath.Split('/').Where(q => !string.IsNullOrEmpty(q)).ToArray();
            XmlNode xNode = null;
            XmlNode xNodeParent = xDoc;
            int i = nodes.Length;
            for (; i > 0; i--)
            {
                string[] nodesNew = new string[i];
                Array.Copy(nodes, nodesNew, i);
                string pathNew = String.Join("/", nodesNew);
                xNode = xDoc.SelectSingleNode(pathNew);
                if (xNode != null)
                {
                    xNodeParent = xNode;
                    break;
                }
            }
            for (int j = i; j < nodes.Length; j++)
            {
                XmlNode xNodeNew = xDoc.CreateElement(nodes[j]);
                xNodeParent.AppendChild(xNodeNew);
                xNodeParent = xNodeNew;
            }
            return xDoc.SelectSingleNode(xpath);
        }

        public static void CreateOrUpdateXmlNodeByXPath(XmlDocument xDoc, string xpath, string value)
        {
            CreateOrUpdateXmlNodeByXPath(xDoc, xpath);
            XmlNode xNode = xDoc.SelectSingleNode(xpath);
            if (xNode != null)
            {
                xNode.InnerText = value;
            }
        }

        public static void CreateOrUpdateXmlAttributeByXPath(XmlDocument xDoc, string xpath, string xAttrName, string value)
        {
            CreateOrUpdateXmlNodeByXPath(xDoc, xpath);
            XmlNode xNode = xDoc.SelectSingleNode(xpath);
            if (xNode != null)
            {
                XmlAttribute xAttribute = xNode.Attributes[xAttrName];
                if (xAttribute != null)
                {
                    xAttribute.Value = value;
                }
                else
                {
                    xAttribute = xDoc.CreateAttribute(xAttrName);
                    xAttribute.Value = value;
                    xNode.Attributes.Append(xAttribute);
                }
            }
        }

        public static XmlNode AddNode(XmlDocument xDoc, XmlNode xNode, string xNodeName, string xNodeText)
        {
            XmlNode xNodeChild = xDoc.CreateElement(xNodeName);
            xNodeChild.InnerText = xNodeText;
            xNode.AppendChild(xNodeChild);
            return xNodeChild;
        }

        public static XmlNode AddNode(XmlDocument xDoc, XmlNode xNode, string xNodeName, string xNodeText, string[] attrNames, string[] attrTexts)
        {
            XmlNode xNodeChild = xDoc.CreateElement(xNodeName);
            xNodeChild.InnerText = xNodeText;
            if (attrNames != null && attrTexts != null && attrNames.Length > 0 && attrNames.Length == attrTexts.Length)
            {
                for (int i = 0; i < attrNames.Length; i++)
                {
                    XmlAttribute xAttr = xDoc.CreateAttribute(attrNames[i]);
                    xAttr.Value = attrTexts[i];
                    xNodeChild.Attributes.Append(xAttr);
                }
            }
            xNode.AppendChild(xNodeChild);
            return xNodeChild;
        }
    }
}