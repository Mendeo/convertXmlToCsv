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
		'inputFile = "data.xml"

		Dim xDoc As New XmlDocument()
		xDoc.Load(inputFile)
		Dim xRoot As XmlElement = xDoc.DocumentElement
		If xRoot Is Nothing Then
			Console.WriteLine("Cannot find root element")
			Return
		End If
		Dim row As New List(Of String)
		Dim sw As New IO.StreamWriter(IO.Path.Combine(IO.Path.GetPathRoot(inputFile), IO.Path.GetFileNameWithoutExtension(inputFile) & ".csv"))

		For Each x1 As XmlElement In xRoot
			For Each x2 As XmlElement In x1
				row.Add(x2.Name)
			Next
			Exit For
		Next
		sw.WriteLine(String.Join(";", row))
		For Each xRow As XmlElement In xRoot
			row.Clear()
			For Each xFiled As XmlElement In xRow
				row.Add(xFiled.InnerText)
			Next
			sw.WriteLine(String.Join(";", row))
		Next
		sw.Flush()
		sw.Close()
	End Sub

End Module
