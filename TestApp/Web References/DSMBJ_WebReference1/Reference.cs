﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 4.0.30319.42000 版自动生成。
// 
#pragma warning disable 1591

namespace TestApp.DSMBJ_WebReference1 {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="DataSwitchImpSoap11Binding", Namespace="http://impl.server.bjdsm.com")]
    public partial class DataSwitchImp : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback queryData4WhereOperationCompleted;
        
        private System.Threading.SendOrPostCallback identityOperationCompleted;
        
        private System.Threading.SendOrPostCallback sendDataOperationCompleted;
        
        private System.Threading.SendOrPostCallback queryDataOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public DataSwitchImp() {
            this.Url = global::TestApp.Properties.Settings.Default.TestApp_DSMBJ_WebReference1_DataSwitchImp;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event queryData4WhereCompletedEventHandler queryData4WhereCompleted;
        
        /// <remarks/>
        public event identityCompletedEventHandler identityCompleted;
        
        /// <remarks/>
        public event sendDataCompletedEventHandler sendDataCompleted;
        
        /// <remarks/>
        public event queryDataCompletedEventHandler queryDataCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:queryData4Where", RequestNamespace="http://impl.server.bjdsm.com", ResponseNamespace="http://impl.server.bjdsm.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", IsNullable=true)]
        public string queryData4Where([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string ak, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string teName, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string whereXml) {
            object[] results = this.Invoke("queryData4Where", new object[] {
                        ak,
                        teName,
                        whereXml});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void queryData4WhereAsync(string ak, string teName, string whereXml) {
            this.queryData4WhereAsync(ak, teName, whereXml, null);
        }
        
        /// <remarks/>
        public void queryData4WhereAsync(string ak, string teName, string whereXml, object userState) {
            if ((this.queryData4WhereOperationCompleted == null)) {
                this.queryData4WhereOperationCompleted = new System.Threading.SendOrPostCallback(this.OnqueryData4WhereOperationCompleted);
            }
            this.InvokeAsync("queryData4Where", new object[] {
                        ak,
                        teName,
                        whereXml}, this.queryData4WhereOperationCompleted, userState);
        }
        
        private void OnqueryData4WhereOperationCompleted(object arg) {
            if ((this.queryData4WhereCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.queryData4WhereCompleted(this, new queryData4WhereCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:identity", RequestNamespace="http://impl.server.bjdsm.com", ResponseNamespace="http://impl.server.bjdsm.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", IsNullable=true)]
        public string identity([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string ak) {
            object[] results = this.Invoke("identity", new object[] {
                        ak});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void identityAsync(string ak) {
            this.identityAsync(ak, null);
        }
        
        /// <remarks/>
        public void identityAsync(string ak, object userState) {
            if ((this.identityOperationCompleted == null)) {
                this.identityOperationCompleted = new System.Threading.SendOrPostCallback(this.OnidentityOperationCompleted);
            }
            this.InvokeAsync("identity", new object[] {
                        ak}, this.identityOperationCompleted, userState);
        }
        
        private void OnidentityOperationCompleted(object arg) {
            if ((this.identityCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.identityCompleted(this, new identityCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:sendData", RequestNamespace="http://impl.server.bjdsm.com", ResponseNamespace="http://impl.server.bjdsm.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", IsNullable=true)]
        public string sendData([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string ak, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string uuid, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string xmlData) {
            object[] results = this.Invoke("sendData", new object[] {
                        ak,
                        uuid,
                        xmlData});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void sendDataAsync(string ak, string uuid, string xmlData) {
            this.sendDataAsync(ak, uuid, xmlData, null);
        }
        
        /// <remarks/>
        public void sendDataAsync(string ak, string uuid, string xmlData, object userState) {
            if ((this.sendDataOperationCompleted == null)) {
                this.sendDataOperationCompleted = new System.Threading.SendOrPostCallback(this.OnsendDataOperationCompleted);
            }
            this.InvokeAsync("sendData", new object[] {
                        ak,
                        uuid,
                        xmlData}, this.sendDataOperationCompleted, userState);
        }
        
        private void OnsendDataOperationCompleted(object arg) {
            if ((this.sendDataCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.sendDataCompleted(this, new sendDataCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:queryData", RequestNamespace="http://impl.server.bjdsm.com", ResponseNamespace="http://impl.server.bjdsm.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", IsNullable=true)]
        public string queryData([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string ak, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string teName) {
            object[] results = this.Invoke("queryData", new object[] {
                        ak,
                        teName});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void queryDataAsync(string ak, string teName) {
            this.queryDataAsync(ak, teName, null);
        }
        
        /// <remarks/>
        public void queryDataAsync(string ak, string teName, object userState) {
            if ((this.queryDataOperationCompleted == null)) {
                this.queryDataOperationCompleted = new System.Threading.SendOrPostCallback(this.OnqueryDataOperationCompleted);
            }
            this.InvokeAsync("queryData", new object[] {
                        ak,
                        teName}, this.queryDataOperationCompleted, userState);
        }
        
        private void OnqueryDataOperationCompleted(object arg) {
            if ((this.queryDataCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.queryDataCompleted(this, new queryDataCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    public delegate void queryData4WhereCompletedEventHandler(object sender, queryData4WhereCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class queryData4WhereCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal queryData4WhereCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    public delegate void identityCompletedEventHandler(object sender, identityCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class identityCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal identityCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    public delegate void sendDataCompletedEventHandler(object sender, sendDataCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class sendDataCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal sendDataCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    public delegate void queryDataCompletedEventHandler(object sender, queryDataCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class queryDataCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal queryDataCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591