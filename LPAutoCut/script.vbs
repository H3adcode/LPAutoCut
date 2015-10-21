Dim argString
argString = ""
If WScript.Arguments.Count > 0 Then
	argString = Wscript.Arguments(0)	
	If WScript.Arguments.Count > 1 Then
		Dim i
		For i = 1 To WScript.Arguments.Count - 1 
			argString = argString & " " & Wscript.Arguments(i)
		Next
	End If
End If
MsgBox argString