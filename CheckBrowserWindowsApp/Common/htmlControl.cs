using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckBrowserWindowsApp.Web
{
    public class Node
    {
        public string name { get; set; }
        public string value { get; set; }

        public override string ToString()
        {
            return $"{{{this.name}}}={this.value}";
        }
    }




    public class htmlControl
    {
        public htmlControl()
        {

        }
        public htmlControl(Microsoft.Web.WebView2.WinForms.WebView2 WebView)
        {
            this._ParentControl = WebView;
        }

        public void SetParentControl(Microsoft.Web.WebView2.WinForms.WebView2 WebView)
        {
            this._ParentControl = WebView;
        }
        protected Microsoft.Web.WebView2.WinForms.WebView2 _ParentControl;
        public string tagName { get; set; }
        public CheckBrowserWindowsApp.Web.Node[] attributes { get; set; }
        public string placeholder { get; set; }
        public string innerHTML { get; set; }
        public string innerText { get; set; }

        public string value { get; set; }
        public string text { get; set; }
        public string serverUniqueID
        {
            get
            {
                return getAttribute("server-unique-id");
            }
        }

        public string getAttribute(string name)
        {
            if (this.attributes == null)
                return "";
            var obj = this.attributes.SingleOrDefault(f => f.name.ToLower() == name.ToLower()); ;
            if (obj == null)
                return "";
            return obj.value;

        }



        /// <summary>
        /// find element child of this element  as JQUERY
        /// </summary>
        /// <param name="selector"> you can use jquery selector condition </param>
        /// <returns></returns>
        public async Task<htmlControl[]> find(string selector)
        {
            string strQuery = $@" ___$find(`[server-unique-id='{this.serverUniqueID}']` ,`{selector}`) ; ";
            var attr = await this._ParentControl.CoreWebView2.ExecuteScriptAsync(strQuery);
            if (attr == null || attr == "null")
                return new htmlControl[] { };

            var arr = (from f in Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(attr)
                       select htmlControl.Parse(this._ParentControl, f)).ToArray();
            return arr;
        }

        /// <summary>
        /// closest elemnt with your condition  as JQUERY
        /// </summary>
        /// <param name="selector"> you can use jquery selector condition </param>
        /// <returns></returns>
        public async Task<htmlControl> closest(string selector)
        {
            string strQuery = $@"  ___$closest(`[server-unique-id='{this.serverUniqueID}']` ,`{selector}`) ; ";
            var attr = await this._ParentControl.CoreWebView2.ExecuteScriptAsync(strQuery);
            if (attr == null || attr == "null")
                return new htmlControl();

            var element = htmlControl.Parse(this._ParentControl, attr);
            return element;
        }

        /// <summary>
        /// set html of this element  as JQUERY
        /// </summary>
        /// <param name="content"> string of html content </param>
        /// <returns></returns>
        public async Task<htmlControl> html(string content)
        {
            string strQuery = $@" ___$html(`[server-unique-id='{this.serverUniqueID}']`,`{content}`) ";
            var attr = await this._ParentControl.CoreWebView2.ExecuteScriptAsync(strQuery);
            if (attr == null || attr == "null")
                return new htmlControl();

            var element = htmlControl.Parse(this._ParentControl, attr);
            return element;
        }

        /// <summary>
        /// set value of this element  as JQUERY
        /// </summary>
        /// <param name="content"> string of html content </param>
        /// <returns></returns>
        public async Task<htmlControl> val(string content)
        {
            string strQuery = $@" ___$val(`[server-unique-id='{this.serverUniqueID}']`,`{content}`) ";
            var attr = await this._ParentControl.CoreWebView2.ExecuteScriptAsync(strQuery);
            if (attr == null || attr == "null")
                return new htmlControl();

            var element = htmlControl.Parse(this._ParentControl, attr);
            return element;
        }

        /// <summary>
        /// Set css of element as JQUERY
        /// </summary>
        /// <param name="name"> name of attribute </param>
        /// <param name="value"> value of attribute </param>
        /// <returns></returns>
        public async Task<htmlControl> css(string name, string value)
        {
            string strQuery = $@" ___$css(`[server-unique-id='{this.serverUniqueID}']` ,`{name}` ,`{value}`) ";
            var attr = await this._ParentControl.CoreWebView2.ExecuteScriptAsync(strQuery);
            if (attr == null || attr == "null")
                return new htmlControl();

            var element = htmlControl.Parse(this._ParentControl, attr);
            return element;
        }

        /// <summary>
        /// Set attribute of element as JQUERY
        /// </summary>
        /// <param name="name"> name of attribute </param>
        /// <param name="value"> value of attribute </param>
        /// <returns></returns>
        public async Task<htmlControl> attr(string name ,string value)
        {
            string strQuery = $@" ___$attr(`[server-unique-id='{this.serverUniqueID}']` ,`{name}` ,`{value}`) ";
            var attr = await this._ParentControl.CoreWebView2.ExecuteScriptAsync(strQuery);
            if (attr == null || attr == "null")
                return new htmlControl();

            var element = htmlControl.Parse(this._ParentControl, attr);
            return element;
        }


        public override string ToString()
        {
            return this.tagName;
        }
        public static htmlControl Parse(Microsoft.Web.WebView2.WinForms.WebView2 WebView, string html)
        {
            var elem = Newtonsoft.Json.JsonConvert.DeserializeObject<htmlControl>(html);
            elem.SetParentControl(WebView);

            if (elem.tagName.ToUpper() == "SELECT")
            {
                var selectelem = Newtonsoft.Json.JsonConvert.DeserializeObject<selectElement>(html);
                selectelem.SetParentControl(WebView);
                return selectelem;
            }
            else if (elem.tagName == "inputElement")
            {
                var inputelem = Newtonsoft.Json.JsonConvert.DeserializeObject<inputElement>(html);
                inputelem.SetParentControl(WebView);
                return inputelem;
            }

            return elem;
        }
    }

    public class inputElement : htmlControl
    {
        public inputElement() { }
        public inputElement(Microsoft.Web.WebView2.WinForms.WebView2 WebView) : base(WebView) { }

        public bool Checked { get; set; }
    }

    public class selectElement : htmlControl
    {
        public selectElement() { }
        public selectElement(Microsoft.Web.WebView2.WinForms.WebView2 WebView) : base(WebView) { }

        public CheckBrowserWindowsApp.Web.htmlControl[] options { get; set; }
        public int selectedIndex { get; set; }

    }


}
