
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
        public static string ApplicationBundle(this Microsoft.Web.WebView2.WinForms.WebView2 webview, string Path = null)
        {
            string result = "";

            string StartupPath = Application.StartupPath;
            string ApplicationName = System.IO.Path.GetFileNameWithoutExtension(Application.ExecutablePath);
            if (string.IsNullOrWhiteSpace(Path))
            {
                //webview.CoreWebView2.SetVirtualHostNameToFolderMapping("demo", Application.StartupPath, CoreWebView2HostResourceAccessKind.DenyCors);
                webview.CoreWebView2.SetVirtualHostNameToFolderMapping(ApplicationName, Application.StartupPath, CoreWebView2HostResourceAccessKind.DenyCors);
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
        public static string ScriptServerEventBundle(this Microsoft.Web.WebView2.WinForms.WebView2 webview)
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



                        const elementsWithDataAttribute = Array.from(document.querySelectorAll('*')).filter(f=> Array.from(f.attributes).filter(x=> x.name.indexOf('server-event-') >= 0).length > 0);
                        for(var i = 0 ; i < elementsWithDataAttribute.length;i++)
                        {{
                            let item = elementsWithDataAttribute[i];
                            let serverUniqueID = ___setServerUniqueID(item);
                            let attrs = Array.from(item.attributes).filter(x=> x.name.indexOf('server-event-') >= 0);
                            for(var j = 0 ; j < attrs.length; j++)
                            {{

                                let eventItem = attrs[j];
                                let eventname = eventItem.name.replace('server-event-' ,'');
                                let bindeventname = eventname;
                                if (bindeventname.startsWith(""on"")){{
                                    bindeventname = bindeventname.substr(2);
                                }}

                                item.addEventListener(bindeventname ,function(e){{ 
                                                                                let __target = e.target;
                                                                                let eArg = {{
                                                                                                eventRealName: e.toString(),
                                                                                                eventName:eventname,
                                                                                                serverEventName :eventItem.value,
                                                                                                elementID : __target.id,
                                                                                                elementName : __target.name,
                                                                                                serverUniqueID : serverUniqueID,
                                                                                                targetOuterHTML : __target.outerHTML,
                                                                                            }};

                                                                                for(var prop in e) 
                                                                                    if(typeof(e[prop]) !== ""object"" && typeof(e[prop]) !== ""function"") 
                                                                                        eArg[prop] = e[prop];

                                                                                window.chrome.webview.postMessage(JSON.stringify(eArg));
                                                                            }});
                            }}

                        }}



                    </script>

                    ";
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


        public async static Task SetDocumentText(this Microsoft.Web.WebView2.WinForms.WebView2 WebView, string styleContent, string bodyContent)
        {

            try
            {
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


    }

}
