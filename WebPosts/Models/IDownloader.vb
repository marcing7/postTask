Public Interface IDownloader(Of T)
    Function GetData() As List(Of T)
End Interface
