Imports Guna.UI2.WinForms
Imports MySql.Data.MySqlClient

Public Class Form3
    Private form2Instance As Form2 ' Declare an instance of Form2

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Example initialization, make sure to adapt it to your actual control names
        Label6 = New Label()
        Label7 = New Label()

        ' Initialize ComboBox1 with options 1, 2, 3
        Guna2ComboBox1.Items.AddRange({"1", "2", "3"})
        Guna2ComboBox1.SelectedIndex = 0 ' Set the default selection (optional)

        TransferDataFromForm2()
    End Sub

    Private Sub TransferDataFromForm2()
        If form2Instance IsNot Nothing AndAlso form2Instance.Guna2DataGridView1.SelectedRows.Count > 0 Then
            ' Assuming you have an instance of Form2 assigned to form2Instance
            Dim dressName As String = form2Instance.Guna2DataGridView1.SelectedRows(0).Cells("dressname").Value.ToString()
            Dim dressSize As String = form2Instance.Guna2DataGridView1.SelectedRows(0).Cells("dresssize").Value.ToString()

            Label6.Text = dressName
            Label7.Text = dressSize

            ' Set the default selection for ComboBox1
            Guna2ComboBox1.SelectedIndex = 0
        End If
    End Sub

    Private Sub Guna2GradientButton2_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton2.Click
        ' Call the method to commit changes to the sales table
        CommitSalesChanges()
    End Sub

    Private Sub CommitSalesChanges()
        Dim name As String = Label6.Text
        Dim size As String = Label7.Text
        Dim qtysold As Integer

        ' Validate if ComboBox1 selected item is a valid integer
        If Integer.TryParse(Guna2ComboBox1.SelectedItem.ToString(), qtysold) Then
            ' Check if sales changes were successfully committed to the database
            If AddSalesToDatabase(name, size, qtysold) Then
                ' Update the quantity in the dress table
                UpdateDressQuantity(name, size, qtysold)
                MessageBox.Show("Sales changes committed successfully!")
            Else
                MessageBox.Show("Failed to commit sales changes. Please check the input.")
            End If
        Else
            ' Display a message for invalid input
            MessageBox.Show("Invalid quantity sold. Please select a valid number from the ComboBox.")
        End If
    End Sub

    Private Function AddSalesToDatabase(name As String, size As String, qtysold As Integer) As Boolean
        Try
            Using connection As New MySqlConnection("Server=localhost;Database=butique;User=root;Password=admin;")
                connection.Open()

                Dim query As String = "INSERT INTO sales (name, size, qtysold) VALUES (@name, @size, @qtysold)"
                Using cmd As New MySqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@name", name)
                    cmd.Parameters.AddWithValue("@size", size)
                    cmd.Parameters.AddWithValue("@qtysold", qtysold)

                    cmd.ExecuteNonQuery()
                    Return True
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}")
            Return False
        End Try
    End Function

    Private Sub UpdateDressQuantity(name As String, size As String, qtysold As Integer)
        Try
            Using connection As New MySqlConnection("Server=localhost;Database=butique;User=root;Password=admin;")
                connection.Open()

                Dim query As String = "UPDATE dress SET dressqty = dressqty - @qtysold WHERE dressname = @name AND dresssize = @size"
                Using cmd As New MySqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@name", name)
                    cmd.Parameters.AddWithValue("@size", size)
                    cmd.Parameters.AddWithValue("@qtysold", qtysold)

                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error updating dress quantity: {ex.Message}")
        End Try
    End Sub

    Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton1.Click
        Form2.Show()
        Me.Hide()
    End Sub

    Private Sub Guna2GradientButton6_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton6.Click
        Me.Close()
    End Sub
End Class
