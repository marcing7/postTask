Imports System.Net
Imports Newtonsoft.Json
Public Class Comment
    Implements IDownloader(Of Comment)
    <JsonProperty("id")>
    Public Property Id As String
    <JsonProperty("postId")>
    Public Property PostId As String
    <JsonProperty("name")>
    Public Property Name As String
    <JsonProperty("email")>
    Public Property Email As String
    <JsonProperty("body")>
    Public Property Body As String

    Private ReadOnly url As String
    Public Sub New()

    End Sub
    Public Sub New(pUrl As String)
        url = pUrl
    End Sub
    Public Function GetData() As List(Of Comment) Implements IDownloader(Of Comment).GetData
        Dim retval = New List(Of Comment)
        Try
            Using wc As New WebClient() With {.Proxy = WebRequest.GetSystemWebProxy()}
                retval = JsonConvert.DeserializeObject(Of IEnumerable(Of Comment))(wc.DownloadString(New Uri(url)), New JsonSerializerSettings With {.TypeNameHandling = TypeNameHandling.Auto})
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return retval
    End Function
End Class
