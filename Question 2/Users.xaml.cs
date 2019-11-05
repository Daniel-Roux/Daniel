using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Question_2
{
    /// <summary>
    /// Interaction logic for Users.xaml
    /// </summary>
    public partial class Users : Window
    {
        public Users()
        {
            InitializeComponent();
            fillgrid();
            txt_id.Visibility = Visibility.Hidden;
            txt_UserName.Visibility = Visibility.Hidden;
            txt_FullName.Visibility = Visibility.Hidden;
            cb_Branch.Visibility = Visibility.Hidden;
            cb_Shifts.Visibility = Visibility.Hidden;
            lblUser.Visibility = Visibility.Hidden;
            lblFull.Visibility = Visibility.Hidden;
            lblShifts.Visibility = Visibility.Hidden;
            lblBranch.Visibility = Visibility.Hidden;

            btn_Cancel.Visibility = Visibility.Hidden;
            btn_Save.Visibility = Visibility.Hidden;
        }

        string addingEditing = "";
        private void Btn_Add_Click(object sender, RoutedEventArgs e)
        {
            btn_Save.Visibility = Visibility.Visible;
            btn_Cancel.Visibility = Visibility.Visible;
            btn_Add.Visibility = Visibility.Hidden;
            btn_Edit.Visibility = Visibility.Hidden;
            btn_Delete.Visibility = Visibility.Hidden;

            addingEditing = "Adding";

            dgv_Users.Visibility = Visibility.Hidden;
            lblUser.Visibility = Visibility.Visible;
            lblFull.Visibility = Visibility.Visible;
            lblShifts.Visibility = Visibility.Visible;
            lblBranch.Visibility = Visibility.Visible;
            txt_UserName.Visibility = Visibility.Visible;
            txt_FullName.Visibility = Visibility.Visible;
            cb_Branch.Visibility = Visibility.Visible;
            cb_Shifts.Visibility = Visibility.Visible;

            txt_UserName.Text = "";
            txt_FullName.Text = "";
            cb_Branch.SelectedValue = -1;
            cb_Shifts.SelectedValue = -1;
        }

        private void Btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            txt_UserName.Visibility = Visibility.Visible;
            txt_FullName.Visibility = Visibility.Visible;
            lblUser.Visibility = Visibility.Visible;
            lblFull.Visibility = Visibility.Visible;
            lblShifts.Visibility = Visibility.Visible;
            lblBranch.Visibility = Visibility.Visible;
            cb_Branch.Visibility = Visibility.Visible;
            cb_Shifts.Visibility = Visibility.Visible;
            dgv_Users.Visibility = Visibility.Hidden;


            btn_Save.Visibility = Visibility.Visible;
            btn_Cancel.Visibility = Visibility.Visible;
            btn_Add.Visibility = Visibility.Hidden;
            btn_Edit.Visibility = Visibility.Hidden;
            btn_Delete.Visibility = Visibility.Hidden;

            addingEditing = "Editing";
        }

        private void Btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete this User?", "Confirm Deletion", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.No) return;
            string constr = ConfigurationManager.ConnectionStrings["Question2"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            using (var cmd = new SqlCommand("DeleteUser", con) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = txt_id.Text.Trim();
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                }
                finally
                {
                    if (con.State == ConnectionState.Open) con.Close();
                }
                fillgrid();
            }
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            if (addingEditing == "Adding")
            {
                string constr = ConfigurationManager.ConnectionStrings["Question2"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                using (var cmd = new SqlCommand("InsertUser", con) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = txt_UserName.Text.Trim();
                    cmd.Parameters.Add("@Fullname", SqlDbType.NVarChar).Value = txt_FullName.Text.Trim();
                    cmd.Parameters.Add("@Branch", SqlDbType.Int).Value = Convert.ToInt32(cb_Branch.SelectedValue);
                    cmd.Parameters.Add("@Shifts", SqlDbType.Int).Value = Convert.ToInt32(cb_Shifts.SelectedValue);
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch
                    {
                    }
                    finally
                    {
                        if (con.State == ConnectionState.Open) con.Close();
                    }
                }
            }
            else
            {
                string constr = ConfigurationManager.ConnectionStrings["Question2"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                using (var cmd = new SqlCommand("UpdateUser", con) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(txt_id.Text);
                    cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = txt_UserName.Text.Trim();
                    cmd.Parameters.Add("@Fullname", SqlDbType.NVarChar).Value = txt_FullName.Text.Trim();
                    cmd.Parameters.Add("@Branch", SqlDbType.Int).Value = Convert.ToInt32(cb_Branch.SelectedValue);
                    cmd.Parameters.Add("@Shifts", SqlDbType.Int).Value = Convert.ToInt32(cb_Shifts.SelectedValue);

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch
                    {
                    }
                    finally
                    {
                        if (con.State == ConnectionState.Open) con.Close();
                    }
                }
            }

            txt_UserName.Visibility = Visibility.Hidden;
            txt_FullName.Visibility = Visibility.Hidden;
            cb_Branch.Visibility = Visibility.Hidden;
            cb_Shifts.Visibility = Visibility.Hidden;
            dgv_Users.Visibility = Visibility.Visible;
            lblUser.Visibility = Visibility.Hidden;
            lblFull.Visibility = Visibility.Hidden;
            lblShifts.Visibility = Visibility.Hidden;
            lblBranch.Visibility = Visibility.Hidden;

            btn_Save.Visibility = Visibility.Hidden;
            btn_Cancel.Visibility = Visibility.Hidden;
            btn_Add.Visibility = Visibility.Visible;
            btn_Edit.Visibility = Visibility.Visible;
            btn_Delete.Visibility = Visibility.Visible;
            addingEditing = "";
            fillgrid();
        }

        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {

            txt_UserName.Visibility = Visibility.Hidden;
            txt_FullName.Visibility = Visibility.Hidden;
            lblUser.Visibility = Visibility.Hidden;
            lblFull.Visibility = Visibility.Hidden;
            lblBranch.Visibility = Visibility.Hidden;
            lblShifts.Visibility = Visibility.Hidden;
            cb_Branch.Visibility = Visibility.Hidden;
            cb_Shifts.Visibility = Visibility.Hidden;
            dgv_Users.Visibility = Visibility.Visible;

            btn_Save.Visibility = Visibility.Hidden;
            btn_Cancel.Visibility = Visibility.Hidden;
            btn_Add.Visibility = Visibility.Visible;
            btn_Edit.Visibility = Visibility.Visible;
            btn_Delete.Visibility = Visibility.Visible;
            addingEditing = "";
            fillgrid();
        }

        private void fillgrid()
        {
            string constr = ConfigurationManager.ConnectionStrings["Question2"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            using (var cmd1 = new SqlCommand("SelectAllBranches", con) { CommandType = CommandType.StoredProcedure })
            {
                con.Open();
                cmd1.ExecuteNonQuery();
                SqlDataAdapter sda = new SqlDataAdapter(cmd1);
                DataTable dt = new DataTable("Branches");
                sda.Fill(dt);
                cb_Branch.DisplayMemberPath = "Branch";
                cb_Branch.SelectedValuePath = "id";
                cb_Branch.ItemsSource = dt.DefaultView;
                //dgv_Users.ItemsSource = null;
                //dgv_Users.ItemsSource = dt.DefaultView;
                con.Close();
            }
            using (SqlConnection con = new SqlConnection(constr))
            using (var cmd2 = new SqlCommand("SelectAllShifts", con) { CommandType = CommandType.StoredProcedure })
            {
                con.Open();
                cmd2.ExecuteNonQuery();
                SqlDataAdapter sda = new SqlDataAdapter(cmd2);
                DataTable dt = new DataTable("Shifts");
                sda.Fill(dt);
                cb_Shifts.DisplayMemberPath = "Shifts";
                cb_Shifts.SelectedValuePath = "id";
                cb_Shifts.ItemsSource = dt.DefaultView;
                //dgv_Users.ItemsSource = null;
                //dgv_Users.ItemsSource = dt.DefaultView;
                con.Close();
            }
            using (SqlConnection con = new SqlConnection(constr))
            using (var cmd = new SqlCommand("SellectAllUsers", con) { CommandType = CommandType.StoredProcedure })
            {
                con.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Users");
                sda.Fill(dt);
                //DataGridTextColumn txt = dgv_Users.Columns[0] as DataGridTextColumn;
                
                dgv_Users.ItemsSource = null;
                dgv_Users.ItemsSource = dt.DefaultView;
                con.Close();
            }
        }

        private void Dgv_Users_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                txt_id.Text = dr["id"].ToString();
                txt_UserName.Text = dr["UserName"].ToString();
                txt_FullName.Text = dr["FullName"].ToString();
                cb_Branch.SelectedValue = dr["fk_Branches"];
                cb_Shifts.SelectedValue = dr["fk_Shifts"];
            }
        }
    }
}
