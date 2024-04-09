Imports System.Data.SqlTypes
Imports System.Net
Imports Newtonsoft.Json

Public Class Post
    Implements IDownloader(Of Post), IEqualityComparer(Of Post)
    <JsonProperty("id")>
    Public Property Id As String
    <JsonProperty("userId")>
    Public Property UserId As String
    <JsonProperty("title")>
    Public Property Title As String
    <JsonProperty("body")>
    Public Property Body As String
    Public Property Comments As List(Of Comment)

    Private ReadOnly url As String
    Public Sub New()

    End Sub
    Public Sub New(pUrl As String)
        url = pUrl
    End Sub
    Public Function GetData() As List(Of Post) Implements IDownloader(Of Post).GetData
        Dim retval = New List(Of Post)
        Try
            Using wc As New WebClient() With {.Proxy = WebRequest.GetSystemWebProxy()}
                retval = JsonConvert.DeserializeObject(Of IEnumerable(Of Post))(wc.DownloadString(New Uri(url)), New JsonSerializerSettings With {.TypeNameHandling = TypeNameHandling.Auto})
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return retval
    End Function

    Public Function Equals(x As Post, y As Post) As Boolean Implements IEqualityComparer(Of Post).Equals
        Return x.Id.Equals(y.Id)
    End Function

    Public Function GetHashCode(obj As Post) As Integer Implements IEqualityComparer(Of Post).GetHashCode
        Throw New NotImplementedException()
    End Function
End Class
