Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient

Public Class Form1
    ' Replace with your actual connection string
    Private connectionString As String = "Server=localhost;Database=butique;User=root;Password=admin;"


    Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton1.Click
        Dim username As String = Guna2TextBox1.Text
        Dim password As String = Guna2TextBox2.Text

        If AuthenticateUser(username, password) Then
            MessageBox.Show("Login successful!")
            ' Perform actions after successful login, such as opening another form, etc.
        Else
            MessageBox.Show("Login failed. Invalid username or password.")
        End If
        Me.Hide()
        Form2.Show()
    End Sub

    Private Function AuthenticateUser(username As String, password As String) As Boolean
        Using connection As New MySqlConnection(connectionString)
            connection.Open()

            Dim query As String = "SELECT COUNT(*) FROM user WHERE username = @username AND password = @password"
            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@username", username)
                cmd.Parameters.AddWithValue("@password", password)

                Dim result As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                Return result > 0
            End Using
        End Using
    End Function
End Class
