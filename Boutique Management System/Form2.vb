﻿Imports MySql.Data.MySqlClient

Public Class Form2
    Public WithEvents Guna2ComboBox1Form2 As Guna.UI2.WinForms.Guna2ComboBox
    Public WithEvents Guna2DataGridViewForm2 As Guna.UI2.WinForms.Guna2DataGridView


    ' Replace with your actual connection string
    Private connectionString As String = "Server=localhost;Database=butique;User=root;Password=admin;"

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Hide the Guna2GroupBox controls on form load
        Guna2GroupBox4.Visible = False
        Guna2GroupBox3.Visible = False
        Guna2GroupBox6.Visible = False
        Guna2GroupBox5.Visible = False

        ' Set options for the Guna2ComboBox
        Guna2ComboBox1.Items.AddRange({"S", "M", "L", "XL", "XXL"})
        Guna2ComboBox1.SelectedIndex = 0 ' Set the default selection (optional)
    End Sub

    Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton1.Click
        Guna2GroupBox3.Visible = True
    End Sub

    Private Sub Guna2GradientButton7_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton7.Click
        Dim dressName As String = Guna2TextBox1.Text
        Dim dressQty As Integer

        If Integer.TryParse(Guna2TextBox2.Text, dressQty) Then
            Dim dressSize As String = Guna2ComboBox1.SelectedItem.ToString()

            If AddDressToDatabase(dressName, dressSize, dressQty) Then
                MessageBox.Show("Dress added successfully!")

                ' Open Form3 with the new dress data
            Else
                MessageBox.Show("Failed to add dress. Please check the input.")
            End If
        Else
            MessageBox.Show("Invalid quantity. Please enter a valid number.")
        End If
    End Sub
    Private Function AddDressToDatabase(name As String, size As String, qty As Integer) As Boolean
        Try
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "INSERT INTO dress (dressname, dresssize, dressqty) VALUES (@dressname, @dresssize, @dressqty)"
                Using cmd As New MySqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@dressname", name)
                    cmd.Parameters.AddWithValue("@dresssize", size)
                    cmd.Parameters.AddWithValue("@dressqty", qty)

                    cmd.ExecuteNonQuery()
                    Return True ' Return true when the insertion is successful
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}")
            Return False ' Return false if an exception occurs
        End Try
    End Function


    Private Sub Guna2GradientButton2_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton2.Click
        Guna2GroupBox4.Visible = True
    End Sub

    Private Sub Guna2GradientButton8_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton8.Click
        LoadDataToDataGridView()
    End Sub

    Private Sub Guna2GradientButton9_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton9.Click
        SaveChangesToDatabase()
    End Sub

    Private Sub Guna2GradientButton10_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton10.Click
        TransferDataToForm3()
    End Sub

    ' Load data from dress table to Guna2DataGridView1
    Private Sub LoadDataToDataGridView()
        Try
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT * FROM dress"
                Using cmd As New MySqlCommand(query, connection)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)

                    Guna2DataGridView1.DataSource = dt
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}")
        End Try
    End Sub

    ' Save changes from Guna2DataGridView1 to the database
    Private Sub SaveChangesToDatabase()
        Try
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT * FROM dress"
                Using cmd As New MySqlCommand(query, connection)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim builder As New MySqlCommandBuilder(adapter)

                    adapter.Update(CType(Guna2DataGridView1.DataSource, DataTable))
                End Using
            End Using

            MessageBox.Show("Changes saved successfully!")
        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}")
        End Try
    End Sub

    ' Transfer selected dress data to Form3
    Private Sub TransferDataToForm3()
        If Guna2DataGridView1.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = Guna2DataGridView1.SelectedRows(0)

            ' Create an instance of Form3
            Dim form3 As New Form3()

            ' Set properties in Form3 using data from the selected row
            form3.Label6.Text = selectedRow.Cells("dressname").Value.ToString()
            form3.Label7.Text = selectedRow.Cells("dresssize").Value.ToString()

            ' Set the text in TextBox1 in Form3
            ' Show Form3
            form3.Show()
        Else
            MessageBox.Show("Please select a dress from the DataGridView.")
        End If
    End Sub

    Private Sub Guna2GradientButton3_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton3.Click
        Guna2GroupBox5.Visible = True
    End Sub

    Private Sub Guna2GradientButton4_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton4.Click
        Guna2GroupBox6.Visible = True
    End Sub
    Private Sub Guna2GradientButton11_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton11.Click
        ' Get the input values from Guna2TextBox4 and Guna2TextBox3
        Dim username As String = Guna2TextBox4.Text
        Dim password As String = Guna2TextBox3.Text

        ' Perform any action with the username and password, for example, print them to the console
        Console.WriteLine($"Username: {username}, Password: {password}")

        ' Call a method to commit changes to the user (you can replace this with your actual logic)
        If CommitUserChanges(username, password) Then
            MessageBox.Show("User changes committed successfully!")
        Else
            MessageBox.Show("Failed to commit user changes. Please check the input.")
        End If
    End Sub

    Private Function CommitUserChanges(username As String, password As String) As Boolean
        Try
            ' Perform the necessary logic to commit changes to the user
            ' Replace the following line with your actual database logic
            ' For example, you might use a MySqlCommand to update the user table in a MySQL database
            ' Here, we are just returning True for demonstration purposes
            Return True
        Catch ex As Exception
            ' Handle any exceptions and display an error message
            MessageBox.Show($"Error: {ex.Message}")
            Return False
        End Try
    End Function
    Private Sub Guna2GradientButton14_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton14.Click
        ' Call the method to load the user table into Guna2DataGridView2
        LoadUserTable()
    End Sub

    Private Sub Guna2GradientButton13_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton13.Click
        ' Call the method to commit changes to the user table
        CommitUserChanges()
    End Sub

    Private Sub LoadUserTable()
        Try
            ' Assuming you have a MySqlConnection and a MySqlCommand for retrieving data
            Using connection As New MySqlConnection("YourConnectionStringHere")
                connection.Open()

                ' Replace the query with your actual SELECT query for the user table
                Dim query As String = "SELECT * FROM user"
                Using cmd As New MySqlCommand(query, connection)
                    ' Assuming you have a DataTable to store the retrieved data
                    Dim dataTable As New DataTable()

                    ' Fill the DataTable with the data from the query
                    Using adapter As New MySqlDataAdapter(cmd)
                        adapter.Fill(dataTable)
                    End Using

                    ' Bind the DataTable to Guna2DataGridView2
                    Guna2DataGridView2.DataSource = dataTable
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error loading user table: {ex.Message}")
        End Try
    End Sub

    Private Sub CommitUserChanges()
        ' Assuming you have a DataTable as the DataSource of Guna2DataGridView2
        Dim dataTable As DataTable = TryCast(Guna2DataGridView2.DataSource, DataTable)

        If dataTable IsNot Nothing Then
            Try
                ' Assuming you have a MySqlCommand for updating changes
                Using connection As New MySqlConnection("YourConnectionStringHere")
                    connection.Open()

                    ' Replace the query with your actual UPDATE query for the user table
                    Dim updateQuery As String = "UPDATE user SET username = @username, password = @password WHERE id = @id"

                    ' Iterate through the rows in the DataTable and update changes
                    For Each row As DataRow In dataTable.Rows
                        Using cmd As New MySqlCommand(updateQuery, connection)
                            cmd.Parameters.AddWithValue("@username", row("username"))
                            cmd.Parameters.AddWithValue("@password", row("password"))
                            cmd.Parameters.AddWithValue("@id", row("id"))

                            cmd.ExecuteNonQuery()
                        End Using
                    Next
                End Using

                MessageBox.Show("User changes committed successfully!")
            Catch ex As Exception
                MessageBox.Show($"Error committing user changes: {ex.Message}")
            End Try
        End If
    End Sub
End Class
