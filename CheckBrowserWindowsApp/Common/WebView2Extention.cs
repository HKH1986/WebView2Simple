
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;


namespace CheckBrowserWindowsApp.Web
{
    public static class WebView2Extention
    {

        /// <summary>
        /// bundle css and js
        /// go to microsoft document https://learn.microsoft.com/en-us/microsoft-edge/webview2/concepts/working-with-local-content?tabs=dotnetcsharp
        /// </summary>
        /// <param name="webview"></param>
        /// <param name="Path"></param>
        /// <returns></returns>
        /// <summary>
        /// bundle css and js
        /// go to microsoft document https://learn.microsoft.com/en-us/microsoft-edge/webview2/concepts/working-with-local-content?tabs=dotnetcsharp
        /// </summary>
        /// <param name="webview"></param>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static string ApplicationBundle(this Microsoft.Web.WebView2.WinForms.WebView2 webview, string Path = null)
        {
            string result = "";

            string StartupPath = Application.StartupPath;
            string ApplicationName = System.IO.Path.GetFileNameWithoutExtension(Application.ExecutablePath);
            if (string.IsNullOrWhiteSpace(Path))
            {
                //webview.CoreWebView2.SetVirtualHostNameToFolderMapping("demo", Application.StartupPath, CoreWebView2HostResourceAccessKind.DenyCors);
                webview.CoreWebView2.SetVirtualHostNameToFolderMapping(ApplicationName, Application.StartupPath, CoreWebView2HostResourceAccessKind.Allow);
                Path = StartupPath + "\\Library";
            }

            var directoryCollection = System.IO.Directory.GetDirectories(Path);
            if (directoryCollection.Length > 0)
            {
                foreach (var folder in directoryCollection)
                {
                    result += ApplicationBundle(webview, folder);
                }
            }

            foreach (var strFileName in System.IO.Directory.GetFiles(Path))
            {
                string ext = System.IO.Path.GetExtension(strFileName);
                if (ext == ".css")
                    result += $@"
                                <link href=""{$"https://{ApplicationName}/" + strFileName.Replace(StartupPath, "").Replace("\\", "/")}"" rel=""stylesheet""> ";
                else if (ext == ".js")
                    result += $@"
                                <script src=""{$"https://{ApplicationName}/" + strFileName.Replace(StartupPath, "").Replace("\\", "/")}""></script> ";
            }

            return result;
        }

        /// <summary>
        /// Injected Server side javascripts
        /// </summary>
        /// <param name="webview"></param>
        /// <returns></returns>
        public static string ScriptServerEventBundle(this Microsoft.Web.WebView2.WinForms.WebView2 WebView)
        {
            return $@"

                    <script>


                        function newGuid()
                        {{
                            function s4()
                            {{
                                return Math.floor((1 + Math.random()) * 0x10000)
                                    .toString(16)
                                    .substring(1);
                            }}
                            return ""ss-s-s-s-sss"".replace(/s/g, s4);
                        }}

                        function ___getProperties(element) {{
                            var arr = [];
                            for (var i = 0 ; i < element.attributes.length;i++) {{
                                var prop = element.attributes[i];
                                arr.push({{
                                    name: prop.name,
                                    value: prop.value
                                }});
                            }}

                            var model = {{
                                tagName: element.tagName,
                                attributes: arr,
                                Checked: element.checked,
                                placeholder: element.placeholder,
                                innerHTML: element.innerHTML,
                                innerText: element.innerText,
                                value:element.value
                            }}

                            if (model.tagName == ""SELECT"") {{
                                var arropt = [];
                                for (var i = 0; i < element.options.length; i++) {{
                                    var opt = element.options[i];
                                    arropt.push({{ value: opt.value, text: opt.text }});
                                }}
                                model.options = arropt;  
                                model.selectedIndex = element.selectedIndex;
                                //model.selectedOptions = element.selectedOptions;
                                //model.selectedIndex = element.selectedIndex;
                                //model.selectedIndex = element.selectedIndex;

                            }}

                            return model;
                        }}
                       
                        function ___setServerUniqueID(element)
                        {{
                            let serverUniqueID = element.getAttribute('server-unique-id');
                            if(serverUniqueID == null || serverUniqueID == '')
                            {{
                                serverUniqueID = newGuid();
                                element.setAttribute('server-unique-id',serverUniqueID);
                            }}
                            return serverUniqueID;
                        }}

                        function ___$find(currentElementselector ,selector)
                        {{
                            var col = [];
                            if (currentElementselector == null || currentElementselector == '')
                            {{
                               col = $(selector); 
                            }}
                            else 
                            {{
                               col = $(currentElementselector).find(selector);
                            }}
                            var arr =[];
                            for(var i = 0 ; i < col.length ; i++)
                            {{
                                var item = col[i];
                                let serverUniqueID = ___setServerUniqueID(item);
                                arr.push(JSON.stringify(___getProperties(item)));
                            }}
                            return arr;
                        }}

                        function ___$closest(currentElementselector ,selector)
                        {{
                            var col = [];
                            if (currentElementselector == null || currentElementselector == '')
                            {{
                               col = $(selector); 
                            }}
                            else 
                            {{
                               col = $(currentElementselector).closest(selector);
                            }}
                            var arr =[];
                            for(var i = 0 ; i < col.length ; i++)
                            {{
                                var item = col[i];
                                let serverUniqueID = ___setServerUniqueID(item);
                                arr.push(___getProperties(item));
                            }}
                            return arr[0];
                        }}

                        function ___$html(currentElementselector ,content)
                        {{
                            var col = $(currentElementselector).html(content);
                            var arr =[];
                            for(var i = 0 ; i < col.length ; i++)
                            {{
                                var item = col[i];
                                let serverUniqueID = ___setServerUniqueID(item);
                                arr.push(___getProperties(item));
                            }}
                            return arr[0];
                        }}

                        function ___$val(currentElementselector ,content)
                        {{
                            var col = $(currentElementselector).val(content);
                            var arr =[];
                            for(var i = 0 ; i < col.length ; i++)
                            {{
                                var item = col[i];
                                let serverUniqueID = ___setServerUniqueID(item);
                                arr.push(___getProperties(item));
                            }}
                            return arr[0];
                        }}

                        function ___$css(currentElementselector ,name ,value)
                        {{
                            var col = $(currentElementselector).css(name ,value);
                            var arr =[];
                            for(var i = 0 ; i < col.length ; i++)
                            {{
                                var item = col[i];
                                let serverUniqueID = ___setServerUniqueID(item);
                                arr.push(___getProperties(item));
                            }}
                            return arr[0];
                        }}

                        function ___$attr(currentElementselector ,name ,value)
                        {{
                            var col = $(currentElementselector).attr(name ,value);
                            var arr =[];
                            for(var i = 0 ; i < col.length ; i++)
                            {{
                                var item = col[i];
                                let serverUniqueID = ___setServerUniqueID(item);
                                arr.push(___getProperties(item));
                            }}
                            return arr[0];
                        }}

                        function ___callServerEvents(eventRealName ,sender ,e ,eventName ,serverUniqueID ,serverEventName)
                        {{
                            let eArg = {{
                                        eventRealName: eventRealName,
                                        eventName: eventName,
                                        elementID : sender.id,
                                        elementName : sender.name,
                                        serverUniqueID : serverUniqueID,
                                        serverEventName :serverEventName,
                                        targetOuterHTML : sender.outerHTML,
                                       }};
                             
                            for(var prop in e) 
                                if(typeof(e[prop]) !== ""object"" && typeof(e[prop]) !== ""function"") 
                                    eArg[prop] = e[prop];
                             
                            window.chrome.webview.postMessage(JSON.stringify(eArg));
                            return true;
                        }}
                        function ___bindServerEvents(control)
                        {{
                             var elementsWithDataAttribute = [];
                             if (control == null)    
                             {{
                                elementsWithDataAttribute = Array.from(document.querySelectorAll('*')).filter(f=> Array.from(f.attributes).filter(x=> x.name.indexOf('server-event-') >= 0).length > 0);
                             }}
                             else 
                             {{
                                elementsWithDataAttribute =  Array.from($(control)).filter(f=> Array.from(f.attributes).filter(x=> x.name.indexOf('server-event-') >= 0).length > 0);
                             }}
                        
                             var arr =[];
                             for(var i = 0 ; i < elementsWithDataAttribute.length;i++)
                             {{
                                 let item = elementsWithDataAttribute[i];
                                 let serverUniqueID = ___setServerUniqueID(item);
                                 let attrs = Array.from(item.attributes).filter(x=> x.name.indexOf('server-event-') >= 0);
                                 for(var j = 0 ; j < attrs.length; j++)
                                 {{
                             
                                     let eventItem = attrs[j];
                                     let _eventname = eventItem.name.replace('server-event-' ,'');
                                     let bindeventname = _eventname;
                                     if (bindeventname.startsWith(""on"")){{
                                         bindeventname = bindeventname.substr(2);
                                     }}
                             
                                     item.addEventListener(bindeventname ,function(e){{ 
                                                                                         return ___callServerEvents(e.toString() ,e.target ,e ,_eventname ,serverUniqueID ,eventItem.value);
                                                                                     }});
                                    
                                 }}
                                 arr.push(JSON.stringify(___getProperties(item)));
                             }}
                            
                            return arr;
                        }}

                        ___bindServerEvents(null);

                    </script>

                    ";
        }

        /// <summary>
        /// Get the file http URL From path
        /// </summary>
        /// <param name="webview"></param>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static string GetUriPath(this Microsoft.Web.WebView2.WinForms.WebView2 webview, string Path)
        {
            string StartupPath = Application.StartupPath;
            string ApplicationName = System.IO.Path.GetFileNameWithoutExtension(Application.ExecutablePath);

            string sUriPath = Path;


            if (sUriPath.StartsWith("~/"))
            {
                sUriPath = sUriPath.Replace("~/", "").Replace(" ", "%20");
            }
            else if (sUriPath.IndexOf(StartupPath) > 0)
            {
                sUriPath = sUriPath.Replace(StartupPath, "").Replace(" ", "%20");
            }
            else
            {

            }

            return $"https://{ApplicationName}/" + sUriPath.Replace(StartupPath, "").Replace("\\", "/");
        }



        /// <summary>
        /// set html of the content into the (BODY)
        /// </summary>
        /// <param name="WebView"> curret web view </param>
        /// <param name="styleContent"> style content in (Style) </param>
        /// <param name="bodyContent"> string content in (Body) </param>
        /// <returns></returns>
        public async static Task SetDocumentText(this Microsoft.Web.WebView2.WinForms.WebView2 WebView, string styleContent, string bodyContent)
        {

            try
            {
                //var options = new CoreWebView2EnvironmentOptions("--allow-file-access-from-files");
                //var environment = await CoreWebView2Environment.CreateAsync(null, null, options);
                //await WebView.EnsureCoreWebView2Async(environment);
                await WebView.EnsureCoreWebView2Async(null);

            }
            catch
            {
                WebView = new Microsoft.Web.WebView2.WinForms.WebView2();
            }

            string strstyle = "";
            if (string.IsNullOrWhiteSpace(styleContent) == false)
            {
                strstyle = (styleContent.IndexOf($"<style") >= 0 ? styleContent : $@" <style> {styleContent} </style> ");
            }

            WebView.NavigateToString($@"

                                        <html>   
                                        <head>
                                            {ApplicationBundle(WebView)}
                                            {strstyle}
                                        </head>
                                        <body>
                                            {bodyContent}
                                            {ScriptServerEventBundle(WebView)}
                                        </body>
                                        </html>

                                       ");
        }

        /// <summary>
        /// find html control in document USE JQUERY
        /// </summary>
        /// <param name="WebView"> curent webview </param>
        /// <param name="selector"> query selector of element </param>
        /// <returns></returns>
        public async static Task<htmlControl[]> findControl(this Microsoft.Web.WebView2.WinForms.WebView2 WebView, string selector)
        {
            string strQuery = $@" ___$find(null,`{selector}`) ; ";
            var attr = await WebView.CoreWebView2.ExecuteScriptAsync(strQuery);
            if (attr == null || attr == "null")
                return null;

            var arr = (from f in Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(attr)
                       select htmlControl.Parse(WebView, f)).ToArray();
            return arr;
        }
        /// <summary>
        /// Execute an javascript function from serverside
        /// </summary>
        /// <param name="WebView"> current web view </param>
        /// <param name="javascript"> javascript as string </param>
        /// <returns></returns>
        public async static Task<string> ExecuteScriptAsync(this Microsoft.Web.WebView2.WinForms.WebView2 WebView, string javascript)
        {
            return await WebView.CoreWebView2.ExecuteScriptAsync(javascript);
        }

        /// <summary>
        /// dynamically bind server events after html of element is changed
        /// </summary>
        /// <param name="WebView"> current webview </param>
        /// <param name="selector"> jquery selector of element  </param>
        /// <returns></returns>
        public async static Task<htmlControl[]> BindServerEvents(this Microsoft.Web.WebView2.WinForms.WebView2 WebView, string selector)
        {
            string strQuery = $@" ___bindServerEvents($(`{selector}`)) ; ";
            var attr = await WebView.CoreWebView2.ExecuteScriptAsync(strQuery);
            if (attr == null || attr == "null")
                return null;

            var arr = (from f in Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(attr)
                       select htmlControl.Parse(WebView, f)).ToArray();
            return arr;
        }

        /// <summary>
        /// dynamically bind server events after html of element is changed (all child of htmlControl)
        /// </summary>
        /// <param name="WebView"> current webview </param>
        /// <param name="Element"> current html as selector </param>
        /// <returns></returns>
        public async static Task<htmlControl[]> BindServerEvents(this Microsoft.Web.WebView2.WinForms.WebView2 WebView, htmlControl Element)
        {
            try
            {
                string strQuery = $@" ___bindServerEvents($(`[server-unique-id='{Element.serverUniqueID}'] *`)) ; ";
                var attr = await WebView.CoreWebView2.ExecuteScriptAsync(strQuery);
                if (attr == null || attr == "null")
                    return null;

                var arr = (from f in Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(attr)
                           select htmlControl.Parse(WebView, f)).ToArray();
                return arr;
            }
            catch
            {
                return null;
            }
        }




        public static object GetEventArgument(this Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs earg)
        {
            string strWebMessage = earg.TryGetWebMessageAsString();
            if (strWebMessage.IndexOf("KeyboardEvent") >= 0)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<KeyboardEvent>(strWebMessage);
            }
            else if (strWebMessage.IndexOf("MouseEvent") >= 0)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<MouseEvent>(strWebMessage);
            }

            return Newtonsoft.Json.JsonConvert.DeserializeObject<UIEvent>(strWebMessage);
        }
        public static async Task<object> InvokeServerEventAsync(this Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs earg, Microsoft.Web.WebView2.WinForms.WebView2 WebView, System.Windows.Forms.Form Form)
        {
            try
            {
                object EV = earg.GetEventArgument();
                UIEvent uiEV = (UIEvent)EV;
                string serverEventName = uiEV.serverEventName;

                //var element = await devToolsContext.QuerySelectorAsync<WebView2.DevTools.Dom.HtmlElement>($@"[server-unique-id=""{uiEV.serverUniqueID}""]");
                var attr = await WebView.CoreWebView2.ExecuteScriptAsync($@" ___getProperties(document.querySelectorAll(`[server-unique-id=""{uiEV.serverUniqueID}""]`)[0]) ; ");
                if (attr == null || attr == "null")
                    return null;

                var element = htmlControl.Parse(WebView, attr);

                MethodInfo[] methods = Form.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
                MethodInfo method = methods.ToList().SingleOrDefault(f => f.Name.ToLower() == serverEventName.ToLower());
                if (method != null)
                {
                    return method.Invoke(Form, new object[] { element, EV });
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }





    }

}
