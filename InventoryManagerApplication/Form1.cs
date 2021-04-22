using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagerApplication
{
    public partial class Form1 : Form
    {
        InvManager Manager = new InvManager();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                //Read the data
                StreamReader streamreader;
                streamreader = File.OpenText("textList.txt");

                string line;
                while ((line = streamreader.ReadLine()) != null)
                {
                    string[] att = line.Split('|');
                    Item itemToAdd = new Item(att[0], att[1], att[2], att[3], double.Parse(att[4]), int.Parse(att[5]), double.Parse(att[6]));

                    //Add the read data to the array
                    Manager.items.Add(itemToAdd);
                }
                //Refresh the data grid view
                if(Manager.items.Count != 0)
                {
                    btn_show.PerformClick();
                }
                streamreader.Close();
            }
            catch
            {
                MessageBox.Show("Please make sure the text file is present");
            }
            
        }
        private void btn_show_Click(object sender, EventArgs e)
        {
            //Empty the grid view
            dataGridView1.DataSource = null;
            //Then put the items in so it won't duplicate
            dataGridView1.DataSource = Manager.items;
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewCell cell = dataGridView1.CurrentCell;
                //Check to see if an item is selected
                if(cell == null)
                {
                    MessageBox.Show("Please select an item to delete");
                }
                if (cell != null)
                {
                    //Find the selected item
                    DataGridViewRow row = cell.OwningRow;
                    string id = (row.Cells["ID"].Value).ToString();
                    Item itemToDelete = null;
                    //Find the id
                    foreach (Item item in Manager.items)
                    {
                        if (item.ID == id)
                        {
                            itemToDelete = item;
                        }
                    }
                    //Perform the delete action
                    if (itemToDelete != null)
                    {
                        Manager.removeItem(itemToDelete);
                        SaveItems();
                        MessageBox.Show("Selected item has been deleted");
                    }
                }
                //Refresh the data grid view
                btn_show.PerformClick();
            }
            catch
            {
                MessageBox.Show("Something went wrong. Item could not be deleted");
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                //Store the entered data
                string ID = txtbox_id.Text;
                string Brand = txtbox_brand.Text;
                string Name = txtbox_name.Text;
                string Type = txtbox_type.Text;
                double Size = double.Parse(txtbox_size.Text);
                int Quantity = int.Parse(txtbox_quantity.Text);
                double Price = double.Parse(txtbox_price.Text);
                //Make sure the entries are not empty
                if (txtbox_id.Text == "" || txtbox_id.Text == null)
                {
                    MessageBox.Show("ID must be filled");
                }
                else if (txtbox_brand.Text == "" || txtbox_brand.Text == null)
                {
                    MessageBox.Show("Brand must be filled");
                }
                else if (txtbox_name.Text == "" || txtbox_name.Text == null)
                {
                    MessageBox.Show("Name must be filled");
                }
                else if (txtbox_type.Text == "" || txtbox_type.Text == null)
                {
                    MessageBox.Show("Type must be filled");
                }
                else
                {
                    Item item = new Item(ID, Brand, Name, Type, Size, Quantity, Price);

                    //Check for duplication of an ID
                    foreach (Item items in Manager.items)
                    {
                        if (items.ID == item.ID)
                        {
                            MessageBox.Show("Item already exists!");
                            return;
                        }


                    }
                    //Perform the action of adding
                    Manager.addItem(item);
                    MessageBox.Show("Item has been added");
                    SaveItems();
                    //Refresh the data grid view
                    btn_show.PerformClick();
                }
                
            }
            catch
            {
                MessageBox.Show("Please make sure every box is filled and every variable is entered correctly");
            }

        }
        private void SaveItems()
        {
            //Save data into a txt file
            Item[] lst = Manager.getAllItems();
            StreamWriter writer = new StreamWriter("textList.txt", false);
            for (int i = 0; i < lst.Length; i++)
            {
                writer.WriteLine(lst[i]);

            }
            writer.Close();
        }
        private void btn_restock_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewCell cell2 = dataGridView1.CurrentCell;
                //Make sure an item is selected
                if (cell2 == null)
                {
                    MessageBox.Show("Please select an item in order to restock");
                }
                else if (cell2 != null)
                {
                    //Find the selected item and store its id
                    DataGridViewRow row = cell2.OwningRow;
                    string id = (row.Cells["ID"].Value).ToString();
                    Item itemToRestock = null;
                    foreach (Item item in Manager.items)
                    {
                        if (item.ID == id)
                        {
                            itemToRestock = item;
                        }
                    }
                    //If the selected item exists then update the quantity of the selected item
                    if (itemToRestock != null)
                    {
                        try
                        {
                            int restock = int.Parse(txtbox_restock.Text);
                            Manager.restockItem(restock, itemToRestock);
                            SaveItems();
                            MessageBox.Show("Selected item has been restocked");
                        }
                        catch
                        {
                            MessageBox.Show("Please enter the quantity in order to restock the selected item");
                        }

                    }
                }
                //Refresh the data grid view
                btn_show.PerformClick();
            }
            catch
            {
                MessageBox.Show("Something went wrong. Please try again");
            }
           
        }
        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_byID_Click(object sender, EventArgs e)
        {
            try
            {
                //Check to see if entry is empty or not
                if (txtbox_byID.Text == "" || txtbox_byID.Text == null)
                {
                    
                    MessageBox.Show("Please enter an ID in order to search by ID");
                    
                }
                else
                {
                    //Store the entry
                    string id = txtbox_byID.Text;
                    Item[] result = new Item[1];
                    //Find the entry
                    for (int i = 0; i < 1; i++)
                    {
                        result[i] = Manager.findByID(id);
                    }
                    //Clean the data grid
                    dataGridView1.DataSource = null;
                    //Display the result
                    dataGridView1.DataSource = result;
                }
               
            }
            catch
            {
                MessageBox.Show("Something went wrong. Please try again.");
            }


        }

        private void btn_byType_Click(object sender, EventArgs e)
        {
            try
            {
                //Check to see if the entry is empty or not
                if (txtbox_byType.Text == "" || txtbox_byType == null)
                {
                    MessageBox.Show("Please enter a type in order to perform a search by type");
                }
                else
                {
                    //Store the entry
                    string type = txtbox_byType.Text;
                    //Check the entry
                    for (int i = 0; i < Manager.findByType(type).Count; i++)
                    {
                        List<Item> result = new List<Item>();
                        result = Manager.findByType(type);
                        //Empty the grid view first in order to prevent mixed up results
                        dataGridView1.DataSource = null;
                        //Display the result
                        dataGridView1.DataSource = result;
                    }
                }
                
            }
            catch
            {
                MessageBox.Show("Something went wrong. Please try again");
            }
            
        }

        private void txtbox_restock_Click(object sender, EventArgs e)
        {
            //Check to see if the user clicked on the textbox or not
            if(txtbox_restock.Focused == true)
            {
                this.txtbox_restock.Text = "";
            }
        }

        private void txtbox_byID_Click(object sender, EventArgs e)
        {
            //Check to see if the user clicked on the textbox or not
            if (txtbox_byID.Focused == true)
            {
                this.txtbox_byID.Text = "";
            }
        }

        private void txtbox_byType_Click(object sender, EventArgs e)
        {
            //Check to see if the user clicked on the textbox or not
            if (txtbox_byType.Focused == true)
            {
                this.txtbox_byType.Text = "";
            }
        }
    }
}
