Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace dxSampleT590924.Models
	Public Class Order
		Public Property OrderID() As Integer
		Public Property OrderDate() As Date
		Public Property CustomerID() As String
		Public Property CustomerName() As String
		Public Property ShipCountry() As String
		Public Property ShipCity() As String
	End Class
	Public Class Customer
		<Required>
		Public Property CustomerID() As Integer

		<Required, MaxLength(10)>
		Public Property FirstName() As String
		<Required, MaxLength(10)>
		Public Property LastName() As String
		Public Property HireDate() As Date
		Public Property Orders() As List(Of Order)
	End Class
End Namespace
