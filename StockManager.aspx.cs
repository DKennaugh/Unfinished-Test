using SQL_Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Netclip_Assessment
{
    public partial class StockManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //add values to all of the lists.
            PopulateList(dropdownBody, "Body_Types");
            PopulateList(dropdownEngine, "Engine_Types");
            PopulateList(dropdownGear, "Gear_Types");

            //If this the edit version of the page AND not the first time it loaded, populate all the fields
            if(Request.QueryString["id"] != null && !IsPostBack)
            {
                Car car = SQLHandler.GetCars(" WHERE cars.id = '" + Request.QueryString["id"] + "';")[0];
                txtName.Text = car.Name;
                dropdownBody.SelectedValue = car.Body;
                dropdownEngine.SelectedValue = car.Engine;
                dropdownGear.SelectedValue = car.Gear;
                textPrice.Text = car.Price.ToString();
                textQty.Text = car.Quantity.ToString();
                textDesc.Text = car.Desc;
                btnDelete.Visible = true;
            }
        }

        //Adds the options to the list from the table
        protected void PopulateList(DropDownList list, String tableName)
        {
            List<String> content;
            
            content = SQLHandler.GetNames(tableName);
            list.Items.Clear();
            foreach (String s in content)
            {
                list.Items.Add(s);
            }
        }

        //Either adds or updates a car
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            //if you didn't leave some fields blank
            if (ValidateEntries())
            {
                //if this page edits a car, send an UPDATE request, else send an INSERT request
                if (Request.QueryString["id"] != null)
                {
                    SQLHandler.UpdateCar(Request.QueryString["id"], txtName.Text, (short)dropdownBody.Items.IndexOf(dropdownBody.SelectedItem), (short)dropdownEngine.Items.IndexOf(dropdownEngine.SelectedItem), (short)dropdownGear.Items.IndexOf(dropdownGear.SelectedItem), Convert.ToDouble(textPrice.Text), Convert.ToInt16(textQty.Text), textDesc.Text);
                } else 
                { 
                    
                    SQLHandler.AddCar(txtName.Text, (short)dropdownBody.Items.IndexOf(dropdownBody.SelectedItem), (short)dropdownEngine.Items.IndexOf(dropdownEngine.SelectedItem), (short)dropdownGear.Items.IndexOf(dropdownGear.SelectedItem), Convert.ToDouble(textPrice.Text), Convert.ToInt16(textQty.Text), textDesc.Text);
                }
                Response.Redirect("Browse");
            }
            
        }

        //Checks if you put the values in the needed fields, also changes the colour of the boxes that are empty
        protected bool ValidateEntries ()
        {
            bool result = true;
            if (txtName.Text.Length == 0)
            {
                txtName.BorderColor = System.Drawing.Color.Red;
                result = false;
            }
            else
            {
                txtName.BorderColor = textPrice.BorderColor;
            }

            if (textImg.Text.Length == 0)
            {
                textImg.BorderColor = System.Drawing.Color.Red;
                result = false;
            }
            else
            {
                textImg.BorderColor = textPrice.BorderColor;
            }
            return result;
        }

        //Asks the database politely to delete the selected car
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            SQLHandler.DeleteCar(Request.QueryString["id"]);
        }
    }
}