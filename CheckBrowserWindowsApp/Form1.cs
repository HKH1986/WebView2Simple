using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Microsoft.Web.WebView2.Core;
using System.Windows.Forms;


using CheckBrowserWindowsApp.Web;




namespace CheckBrowserWindowsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ~Form1()
        {

        }


        private async void Form1_Load(object sender, EventArgs e)
        {

            await this.webViewMain.SetDocumentText(styleContent: $@" 

                                                                        <style>
                                                                            .trhover:hover
                                                                            {{
                                                                                background-color:red;
                                                                            }}
            
                                                                            table tr:hover
                                                                            {{
                                                                                background-color:red;
                                                                            }}
        
                                                                        </style>
                                          
                                                                "


                                    , bodyContent: $@"

                                                        <div id=""div_Main"">
                                                            <div class=""myelement""> this is test HKH </div> 
                                                            <table>
                                                                <tr class=""trhover"">
                                                                    <td>
                                                                        this is test
                                                                    </td>
                                                                </tr>
                                                            </table>

                                                            <button server-event-onmouseup=""mybuttonMouseUp"" id=""btn_1"" name=""btn_myeventname"" server-unique-id=""staticbtnsss"" > this is test 2 </button>
                                                            <input type=""text"" server-event-onkeydown=""mydataset"" onkeypress=""debugger; var e = event; ""   onkeyup=""debugger; var e = event; "" style=""background-color:lightblue;"" />

                                                            <select server-event-onchange=""mydropdown__change"">
                                                                <option >  </option> 
                                                                <option value=""1""> 1 </option> 
                                                                <option value=""5""> 5 </option> 
                                                                <option value=""10""> 10 </option> 
                                                                <option value=""20""> 20 </option> 
                                                            </select>
                                                        </div>

                                                    ");




        }

        private void webViewMain_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            this.webViewMain.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
        }
        private async void webViewMain_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {

            var obj = await e.InvokeServerEventAsync(this.webViewMain, this);
        }
        private void mybuttonMouseUp(object sender, CheckBrowserWindowsApp.Web.MouseEvent e)
        {

            if (e.MouseButton == MouseButton.Right)
            {
                MessageBox.Show("you clicked right");
            }

        }
        private void mydropdown__change(object sender, CheckBrowserWindowsApp.Web.UIEvent e)
        {
            var select = (CheckBrowserWindowsApp.Web.selectElement)sender;
            MessageBox.Show($@" SelectedIndex = {select.selectedIndex.ToString()} , SelectedValue = {select.options[select.selectedIndex].value} ");
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var inputControls = await this.webViewMain.findControl("input");
                await inputControls[0].val("123");
                await inputControls[0].css("color", "navy");


                var buttonControls = await this.webViewMain.findControl("button");
                var result = await buttonControls[0].html(" Hossein Khoddami ");
                var parentdiv = await buttonControls[0].closest("div");
                parentdiv = await parentdiv.attr("data-test-value", "0082713189");

                var divCollection = await this.webViewMain.findControl("div");
                var newdiv = await divCollection[divCollection.Count() - 1].html($@" <div style=""background-color:blue; width:200px; height:200px;""> injected  HTML </div> ");




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
