Imports dxSampleT590924.Models
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc

Namespace dxSampleT590924.Controllers
	Public Class HomeController
		Inherits Controller

		Public Function Index() As ActionResult
			Return View(New Customer() With {.CustomerID = 1})
		End Function
		<HttpPost>
		Public Function PostCustomer(ByVal c As Customer) As ActionResult
			If ModelState.IsValid Then
				Return View("Success", c)
			Else
				Return View("Index", c)
			End If
		End Function
	End Class
End Namespace
