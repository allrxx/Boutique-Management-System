Imports MySql.Data.MySqlClient

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

End Class
