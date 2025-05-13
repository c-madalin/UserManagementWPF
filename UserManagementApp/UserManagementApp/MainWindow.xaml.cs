using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using UserManagementApp.Data;
using UserManagementApp.Models;

namespace UserManagementApp
{
    public partial class MainWindow : Window
    {
        private UserDbContext _context;
        private User _selectedUser;

        public MainWindow()
        {
            InitializeComponent();
            _context = new UserDbContext();
            LoadUsers();
        }

        private void LoadUsers()
        {
            usersDataGrid.ItemsSource = _context.Users.ToList();
        }

        private void ClearForm()
        {
            txtUsername.Text = string.Empty;
            txtPassword.Password = string.Empty;
            _selectedUser = null;
            btnAdd.IsEnabled = true;
            btnUpdate.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Password))
                {
                    MessageBox.Show("Username and password are required.");
                    return;
                }

                var newUser = new User
                {
                    Username = txtUsername.Text,
                    Password = txtPassword.Password
                };

                _context.Users.Add(newUser);
                _context.SaveChanges();

                LoadUsers();
                ClearForm();
                MessageBox.Show("User added successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding user: {ex.Message}");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_selectedUser == null)
                {
                    MessageBox.Show("Please select a user to update.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Password))
                {
                    MessageBox.Show("Username and password are required.");
                    return;
                }

                _selectedUser.Username = txtUsername.Text;
                _selectedUser.Password = txtPassword.Password;

                _context.SaveChanges();
                LoadUsers();
                ClearForm();
                MessageBox.Show("User updated successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating user: {ex.Message}");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_selectedUser == null)
                {
                    MessageBox.Show("Please select a user to delete.");
                    return;
                }

                var result = MessageBox.Show("Are you sure you want to delete this user?",
                    "Confirmation", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    _context.Users.Remove(_selectedUser);
                    _context.SaveChanges();
                    LoadUsers();
                    ClearForm();
                    MessageBox.Show("User deleted successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting user: {ex.Message}");
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void usersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedUser = usersDataGrid.SelectedItem as User;

            if (_selectedUser != null)
            {
                txtUsername.Text = _selectedUser.Username;
                txtPassword.Password = _selectedUser.Password;
                btnAdd.IsEnabled = false;
                btnUpdate.IsEnabled = true;
                btnDelete.IsEnabled = true;
            }
        }
    }
}