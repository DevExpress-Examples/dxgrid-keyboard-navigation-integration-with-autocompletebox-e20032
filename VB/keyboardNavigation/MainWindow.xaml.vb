Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Core.Native
Imports System.Windows.Threading
Imports System.Net
Imports System.Windows.Interactivity

Namespace keyboardNavigation
	''' <summary>
	''' Interaction logic for MainWindow.xaml
	''' </summary>
	Partial Public Class MainWindow
		Inherits Window
		Public Sub New()
			InitializeComponent()
			gridControl1.ItemsSource = TestDataList.Create()
		End Sub

		Private Sub KeyboardNavigationBehavior_FocusEditor(ByVal sender As Object, ByVal e As FocusEditorEventArgs)
			Dim ed As AutoCompleteBox = CType(e.Editor, AutoCompleteBox)
			Dim mainEditor As TextBox = TryCast(ed.Template.FindName("Text", ed), TextBox)
			mainEditor.Focus()
		End Sub

		Private Sub KeyboardNavigationBehavior_FocusEditor_1(ByVal sender As Object, ByVal e As FocusEditorEventArgs)
			Dim sl As Slider = CType(e.Editor, Slider)
			sl.Focus()
		End Sub
	End Class
	Public Class TestDataList
		Inherits List(Of TestDataItem)
		Public Shared Function Create() As TestDataList
			Dim res As New TestDataList()
			For i As Integer = 0 To 1
				Dim item As New TestDataItem()
				item.ID = i
				item.Value = "AA"
				item.Value2 = 2
				res.Add(item)
			Next i
			For i As Integer = 0 To 1
				Dim item As New TestDataItem()
				item.ID = i
				item.Value = "AB"
				item.Value2 = 3
				res.Add(item)
			Next i
			For i As Integer = 0 To 1
				Dim item As New TestDataItem()
				item.ID = i
				item.Value = "AAA"
				item.Value2 = 5
				res.Add(item)
			Next i
			For i As Integer = 0 To 1
				Dim item As New TestDataItem()
				item.ID = i
				item.Value = "ABA"
				item.Value2 = 8
				res.Add(item)
			Next i
			Return res
		End Function
	End Class
	Public Class TestDataItem
        Shared privateDomain As List(Of String) = Nothing
		Public Shared Property Domain() As List(Of String)
			Get
				Return privateDomain
			End Get
			Set(ByVal value As List(Of String))
				privateDomain = value
			End Set
		End Property
		Private privateID As Integer
		Public Property ID() As Integer
			Get
				Return privateID
			End Get
			Set(ByVal value As Integer)
				privateID = value
			End Set
		End Property
		Private value_Renamed As String

		Public Property Value() As String
			Get
				Return Me.value_Renamed
			End Get
			Set(ByVal value As String)
				Me.value_Renamed = value
				If Domain Is Nothing Then
					Domain = New List(Of String) (New String() {Me.value_Renamed})
				End If
				If Domain IsNot Nothing AndAlso Not(Domain.Contains(value)) Then
					Domain.Add(Me.value_Renamed)
				End If
			End Set
		End Property

		Private privateValue2 As Double
		Public Property Value2() As Double
			Get
				Return privateValue2
			End Get
			Set(ByVal value As Double)
				privateValue2 = value
			End Set
		End Property
	End Class

	Public Class KeyboardNavigationBehavior
		Inherits Behavior(Of TableView)
		Public Shared ReadOnly ColumnNameProperty As DependencyProperty = DependencyProperty.Register("ColumnName", GetType(String), GetType(KeyboardNavigationBehavior), New PropertyMetadata(String.Empty))
		Public Property ColumnName() As String
			Get
				Return CStr(GetValue(ColumnNameProperty))
			End Get
			Set(ByVal value As String)
				SetValue(ColumnNameProperty, value)
			End Set
		End Property
		Public ReadOnly Property View() As TableView
			Get
				Return AssociatedObject
			End Get
		End Property
		Public Event FocusEditor As FocusEditorEventHandler
		Protected Overrides Sub OnAttached()
			MyBase.OnAttached()
			AddHandler View.ShowingEditor, AddressOf View_ShowingEditor
		End Sub
		Protected Overrides Sub OnDetaching()
			RemoveHandler View.ShowingEditor, AddressOf View_ShowingEditor
			MyBase.OnDetaching()
		End Sub
		 Private Sub View_ShowingEditor(ByVal sender As Object, ByVal e As ShowingEditorEventArgs)
			If e.Column.Name <> ColumnName Then
				Return
			End If
				Dim cp As GridCellContentPresenter = CType(View.GetCellElementByRowHandleAndColumn(e.RowHandle, e.Column), GridCellContentPresenter)
			Dim editor As FrameworkElement = LayoutHelper.FindElementByName(cp, "PART_Editor")
			RaiseEvent FocusEditor(Me, New FocusEditorEventArgs(editor))
		 End Sub
	End Class
	Public Delegate Sub FocusEditorEventHandler(ByVal sender As Object, ByVal e As FocusEditorEventArgs)
	Public Class FocusEditorEventArgs
		Inherits EventArgs
		Private privateEditor As FrameworkElement
		Public Property Editor() As FrameworkElement
			Get
				Return privateEditor
			End Get
			Private Set(ByVal value As FrameworkElement)
				privateEditor = value
			End Set
		End Property
		Public Sub New(ByVal editor As FrameworkElement)
			Editor = editor
		End Sub
	End Class
End Namespace
