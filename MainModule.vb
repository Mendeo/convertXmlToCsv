Option Strict On
Option Explicit On

Imports System.Xml
Module MainModule

	Sub Main(cargs() As String)
		Dim outputFolder As String = My.Application.Info.DirectoryPath
		If cargs Is Nothing OrElse cargs.Length = 0 OrElse String.IsNullOrEmpty(cargs(0)) Then
			Console.WriteLine("You need to set the input file")
			Return
		End If
		Dim inputFile As String = cargs(0)
		'Dim inputFile As String = "data_null.xml"

		Dim xDoc As New XmlDocument()
		xDoc.Load(inputFile)
		Dim xRoot As XmlElement = xDoc.DocumentElement
		If xRoot Is Nothing Then
			Console.WriteLine("Cannot find root element")
			Return
		End If
		Dim sw As New IO.StreamWriter(IO.Path.Combine(IO.Path.GetPathRoot(inputFile), IO.Path.GetFileNameWithoutExtension(inputFile) & ".csv"))

		'Поиск заголовков
		Dim maxLength As Integer = -1
		Dim headers() As String = Nothing
		Dim rowHeaders As New List(Of String)
		For Each xRow As XmlElement In xRoot
			rowHeaders.Clear()
			For Each xFiled As XmlElement In xRow
				rowHeaders.Add(xFiled.Name)
			Next
			If rowHeaders.Count > maxLength Then
				ReDim headers(rowHeaders.Count - 1)
				rowHeaders.CopyTo(headers)
			End If
		Next
		sw.WriteLine(String.Join(";", headers))

		'Console.WriteLine("Headers: ")
		'For i As Integer = 0 To headers.Length - 1
		'	Console.WriteLine(headers(i))
		'Next
		'Console.ReadLine()

		Dim rowData(headers.Length - 1) As String
		For Each xRow As XmlElement In xRoot
			For i As Integer = 0 To headers.Length - 1
				Dim data As String = If(xRow(headers(i)) Is Nothing, "", xRow(headers(i)).InnerText)
				rowData(i) = data
			Next
			sw.WriteLine(String.Join(";", rowData))
		Next
		sw.Flush()
		sw.Close()
	End Sub

End Module
