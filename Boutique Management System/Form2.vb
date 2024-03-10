Imports MySql.Data.MySqlClient

Public Class Form2
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
                    Return True
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}")
            Return False
        End Try
    End Function


    Private Sub Guna2GradientButton2_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton2.Click
        Guna2GroupBox4.Visible = True
    End Sub
End Class
