Set args = WScript.Arguments.Named
userId = args.Item("userId")
password = args.Item("password")
Call WriteToFile(userId, password)

Sub WriteToFile(userId, password)
	Set objFSO=CreateObject("Scripting.FileSystemObject")

	' How to write file
	outFile="D:\Sai\Projects\RXPL.AD.Service\out.txt"
	Set objFile = objFSO.CreateTextFile(outFile,True)
	objFile.Write "UserId: " & userId & vbCrLf & "Password: " & password & vbCrlf
	objFile.Close
	
End Sub