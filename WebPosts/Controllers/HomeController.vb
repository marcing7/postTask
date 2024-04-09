Imports System.Net
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class HomeController
    Inherits System.Web.Mvc.Controller

    Function Index() As JsonResult
        Dim postCollection As IDownloader(Of Post) = New Post(ConfigurationManager.AppSettings.Get("posts"))
        Dim commentsCollection As IDownloader(Of Comment) = New Comment(ConfigurationManager.AppSettings.Get("comments"))
        Dim result = New List(Of Post)
        Dim query = (From post In postCollection.GetData()
                     Join comment In commentsCollection.GetData() On comment.PostId Equals post.Id
                     Select New With {post, comment}).GroupBy(Function(x) x.post)
        For Each queryResult In query
            result.Add(New Post() With {
                       .Id = queryResult.Key.Id,
                       .UserId = queryResult.Key.UserId,
                       .Body = queryResult.Key.Body,
                       .Title = queryResult.Key.Title,
                       .Comments = queryResult.Select(Function(x) x.comment).ToList()
                       })

        Next
        Return Json(result.ToArray, "application/json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet)
    End Function
End Class
