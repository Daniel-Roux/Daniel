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
    /// Interaction logic for Shifts.xaml
    /// </summary>
    public partial class Shifts : Window
    {
        public Shifts()
        {
            InitializeComponent();
            fillgrid();
            txt_id.Visibility = Visibility.Hidden;
            txt_id.Visibility = Visibility.Hidden;
            label.Visibility = Visibility.Hidden;
            txt_shifts.Visibility = Visibility.Hidden;

            btn_cancel.Visibility = Visibility.Hidden;
            btn_save.Visibility = Visibility.Hidden;
        }
        private void fillgrid()
        {
            string constr = ConfigurationManager.ConnectionStrings["Question2"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            using (var cmd = new SqlCommand("SelectAllShifts", con) { CommandType = CommandType.StoredProcedure })
            {
                con.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Users");
                sda.Fill(dt);
                dgv_Shifts.ItemsSource = null;
                dgv_Shifts.ItemsSource = dt.DefaultView;
                con.Close();
            }
        }
        string addingEditing = "";
        private void Btn_save_Click(object sender, RoutedEventArgs e)
        {
            if (addingEditing == "Adding")
            {
                string constr = ConfigurationManager.ConnectionStrings["Question2"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                using (var cmd = new SqlCommand("InsertShifts", con) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add("@Shifts", SqlDbType.NVarChar).Value = txt_shifts.Text.Trim();
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
                    ResetAll();
                }
            }
            else
            {
                string constr = ConfigurationManager.ConnectionStrings["Question2"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                using (var cmd = new SqlCommand("UpdateShifts", con) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = txt_id.Text.Trim();
                    cmd.Parameters.Add("@Shifts", SqlDbType.NVarChar).Value = txt_shifts.Text.Trim();
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
                    ResetAll();
                }
            }
            txt_shifts.Visibility = Visibility.Hidden;
            label.Visibility = Visibility.Hidden;
            dgv_Shifts.Visibility = Visibility.Visible;

            btn_save.Visibility = Visibility.Hidden;
            btn_cancel.Visibility = Visibility.Hidden;
            btn_add.Visibility = Visibility.Visible;
            btn_edit.Visibility = Visibility.Visible;
            btn_delete.Visibility = Visibility.Visible;
            addingEditing = "";
            fillgrid();
        }
 
        private void Btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            txt_shifts.Visibility = Visibility.Hidden;
            label.Visibility = Visibility.Hidden;
            dgv_Shifts.Visibility = Visibility.Visible;

            btn_save.Visibility = Visibility.Hidden;
            btn_cancel.Visibility = Visibility.Hidden;
            btn_add.Visibility = Visibility.Visible;
            btn_edit.Visibility = Visibility.Visible;
            btn_delete.Visibility = Visibility.Visible;
            addingEditing = "";
            fillgrid();
        }
        private void Btn_add_Click(object sender, RoutedEventArgs e)
        {
            btn_save.Visibility = Visibility.Visible;
            btn_cancel.Visibility = Visibility.Visible;
            btn_add.Visibility = Visibility.Hidden;
            btn_edit.Visibility = Visibility.Hidden;
            btn_delete.Visibility = Visibility.Hidden;

            addingEditing = "Adding";

            dgv_Shifts.Visibility = Visibility.Hidden;
            txt_shifts.Visibility = Visibility.Visible;
            label.Visibility = Visibility.Visible;

            addingEditing = "";
        }

        private void Btn_edit_Click(object sender, RoutedEventArgs e)
        {

            btn_save.Visibility = Visibility.Visible;
            btn_cancel.Visibility = Visibility.Visible;
            btn_add.Visibility = Visibility.Hidden;
            btn_edit.Visibility = Visibility.Hidden;
            btn_delete.Visibility = Visibility.Hidden;

            addingEditing = "Editing";

            dgv_Shifts.Visibility = Visibility.Hidden;
            txt_shifts.Visibility = Visibility.Visible;
            label.Visibility = Visibility.Visible;

            addingEditing = "";
        }

        private void Btn_delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete this Shift?", "Confirm Deletion", MessageBoxButton.YesNo);
            string constr = ConfigurationManager.ConnectionStrings["Question2"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            using (var cmd = new SqlCommand("DeleteShifts", con) { CommandType = CommandType.StoredProcedure })
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
                ResetAll();
            }
        }

        private void Dgv_Shifts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                txt_shifts.Text = dr["Shifts"].ToString();
                txt_id.Text = dr["id"].ToString();
            }
        }
        private void ResetAll()
        {
            txt_id.Text = "";
            txt_shifts.Text = "";
        }

    }
}
