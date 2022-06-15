using SQL_Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Netclip_Assessment
{
    public partial class Browse : System.Web.UI.Page
    {
        List<Car> cars;
        int pageNum;
        readonly int numShown = 10;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.cars = SQLHandler.GetCars(GetConditions());
            //First time this page loads
            if (!this.IsPostBack)
            {
                //This might be the only time this variable is useful, I don't know anymore
                pageNum = 1;
                //Fill the checkbox lists
                PopulateList(RBLBody, "Body_Types");
                PopulateList(RBLEngine, "Engine_Types");
                PopulateList(RBLGear, "Gear_Types");
                //figure out the total number of pages
                RecalculatePages();
                
            }
        }
        
        //Grabs the list of options from the relevant table so I can guarentee a 1:1 corrolation
        protected void PopulateList(CheckBoxList list, String tableName)
        {
            List<String> content;

            content = SQLHandler.GetNames(tableName);
            list.Items.Clear();
            foreach (String s in content)
            {
                list.Items.Add(s);
            }
        }

        //Fills the "Browse" div with blocks for each car in the current selection
        private void PopulateShop (int page)
        {
            String boxCode = "";
            //I'd use a SELECT TOP 10 sql function here, but the software was fighting me.
            //This scales badly, but it works
            for (int i = Math.Max(0 + (page - 1) * (numShown),0); i < (page) * numShown && i < cars.Count; i++)
            {
                boxCode += ""
                    + "<div class='car-container' id='car" + cars[i].Id + "'>"
                        + "<input type='checkbox' class='accordian' id='check" + cars[i].Id + "' style='display: none'>"
                        + "<label for='check" + cars[i].Id + "'>"
                            + "<h4>" + cars[i].Name + "</h4>"
                            + "<h5>R " + String.Format("{0:n}", cars[i].Price) + "</h5>"
                        + "</label>"
                        + "<div class='panel'>"
                            + "<p>" + HTMLify(cars[i].Desc) + "</p>"
                            + "<a href='StockManager?id=" + cars[i].Id + "'>Edit</a>"
                        + "</div>"
                    + "</div>";
                    
            }
            browserBox.InnerHtml = boxCode;
        }

        //Apparently \n doesn't work in HTML
        private String HTMLify(string input)
        {
            return input.Replace("\n", "<br />");
        }

        //Outputs the checked checkboxes of a checkbox list to its own list... of checkboxes
        List<String> GetChecked (CheckBoxList list)
        {
            
            List<String> shortlist = new List<String>();
            foreach (ListItem c in list.Items)
            {
                if (c.Selected)
                {
                    shortlist.Add(c.Value);
                }
            }
            return shortlist;
        }

        //Returns the WHERE portion of the SQL Query
        String GetConditions ()
        {
            String temp = "";
            //Adds each checked checkbox to a list
            List<String> body = GetChecked(RBLBody);
            List<String> engine = GetChecked(RBLEngine);
            List<String> gear = GetChecked(RBLGear);

            //If any body types OR engine types OR gear types are selected OR the In Stock check is toggled
            if (body.Count > 0 || engine.Count > 0 || gear.Count > 0 || hasCar.Checked)
            {
                //Start the statement
                temp = " WHERE ";
                //If there are and body types selected
                if (body.Count > 0)
                {
                    //For the guarenteed 1st line
                    temp += "(Body_Types.Name = '" + body[0] + "'";
                    for (int i = 1; i < body.Count; i++)
                    {
                        //for subsequent possible additional parts
                        temp += " OR Body_Types.Name = '" + body[i] + "'";
                    }
                    temp += ")";
                }

                //Start the statement
                if (engine.Count > 0)
                {
                    //if the above block exists we must have an AND
                    if (body.Count > 0)
                    {
                        temp += " AND ";
                    }
                    //           ''                ''
                    temp += "(Engine_Types.Name = '" + engine[0] + "'";
                    for (int i = 1; i < engine.Count; i++)
                    {
                        temp += " OR Engine_Types.Name = '" + engine[i] + "'";
                    }
                    temp += ")";
                }

                //           ''                ''
                if (gear.Count > 0)
                {
                    //           ''                ''
                    if (body.Count > 0 || engine.Count > 0)
                    {
                        temp += " AND ";
                    }
                    //           ''                ''
                    temp += "(Gear_Types.Name = '" + gear[0] + "'";
                    for (int i = 1; i < gear.Count; i++)
                    {
                        temp += " OR Gear_Types.Name = '" + gear[i] + "'";
                    }
                    temp += ")";
                }
                //           ''                ''
                if (hasCar.Checked)
                {
                    //           ''                ''
                    if (body.Count > 0 || engine.Count > 0 || gear.Count > 0)
                    {
                        temp += " AND ";
                    }
                    //This is how you know if cars exists, if there is at least 1 of them :P
                    temp += "Cars.Quantity > 0";
                }
            }

            return temp;
        }

        //Updates the page counter/selector drop down
        private void RecalculatePages ()
        {
            //Empties the list of cars
            ddlList.Items.Clear();
            //Checks how many integer pages (rounded up) it will take to contain the current list of cars.
            for (int i = 1; i <= Math.Ceiling(cars.Count*1.0/numShown); i++)
            {
                ddlList.Items.Add(i.ToString());
            }
            //The page is set to 2;
            ddlList.SelectedValue = "1";
            //Updates the page to show page 1 of the new list
            PopulateShop(1);
        }

        //Updates the page when the Body Type Options are changed.
        protected void RBLBody_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecalculatePages();
        }

        //Updates the page when the Engine Options are changed.
        protected void RBLEngine_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecalculatePages();
        }

        //Updates the page when the Gear Options are changed.
        protected void RBLGear_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecalculatePages();
        }

        //Updates the page when the In Stock Checkbox is toggled.
        protected void HasCar_CheckedChanged(object sender, EventArgs e)
        {
            RecalculatePages();
        }

        //This is incredibly stupid, but it works
        protected void BtnPrev_Click(object sender, EventArgs e)
        {
            //Changes the value in the selection box, because otherwise the "pageNum" it references is somehow not changed until this method ends, despite me changing it before it's called.
            ddlList.SelectedValue = Math.Max(1, int.Parse(ddlList.SelectedValue) - 1).ToString();
            PopulateShop(int.Parse(ddlList.SelectedValue));
        }

        //So is this
        protected void BtnNext_Click(object sender, EventArgs e)
        {
            //Changes the value in the selection box, because otherwise the "pageNum" it references is somehow not changed until this method ends, despite me changing it before it's called.
            ddlList.SelectedValue = Math.Min((int)Math.Ceiling(cars.Count * 1.0 / numShown), int.Parse(ddlList.SelectedValue) + 1).ToString();
            PopulateShop(int.Parse(ddlList.SelectedValue));
        }

        //This works as intended, which is why the above two methods are an abomination
        protected void DDLList_SelectedIndexChanged(object sender, EventArgs e)
        {
            pageNum = int.Parse(ddlList.SelectedValue);
            PopulateShop(pageNum);
        }
    }

    
}

