   M S B A M L   `   `   `  ����G  CMercatikaApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:MercatikaApp.ViewModelMercatikaApp.ViewModel  5      A%clr-namespace:MercatikaApp.ConvertersMercatikaApp.Converters  5      '    !MercatikaApp.Views.OrdersListView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      3vm$clr-namespace:MercatikaApp.ViewModel     5      7local%clr-namespace:MercatikaApp.Converters     5      
�������  � 	Resources  5         BoolToVisibilityConverter>   8MercatikaApp.Converters.InverseBoolToVisibilityConverter $  InverseBoolToVisibilityConverter��%:   &	        &	      �� 5	   
   6'     5
   
   61   5       � DataContext 5      +   %MercatikaApp.ViewModel.OrderViewModel  5   
   .��5      � .+�5   
   � y�6   �� *IsDetailMode+ �� 	Converter# [ .+�5      ]� $��5q�6   .+�5      ��  �� Orientation$ 
Horizontal=�6   $
��Left=�66   $	��Top=�6Q   $��0,0,0,10q�6f   .+�5      �� $��Recargar��6   $	��120��6V   $��30��6b   ��60   �� *LoadOrdersCommand+6b   5      &   System.Windows.Controls.DataGrid  5         AutoGenerateColumns$ False��5         
IsReadOnly$
 True��5      ��5      �� *Orders+ �� SelectedItem 5      �� *SelectedOrder+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6-   	   Width5  /System.Windows.Controls.DataGridLengthConverter$
	 Auto 6U   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 69   �� *	OrderId+6U     5       $ Cliente��6-   $
	 Auto 6[   
 6>   �� *
ClientId+6[     5!      $ Empleado��6-   $
	 Auto 6^   
 6?   �� *
EmployeeId+6^     5"      $ Fecha Orden��6-   $
	 Auto 6p   
 6B   �� *	OrderDate+ �� StringFormat$ d��6p     5#      $ 
Dirección��6-   $		 200 6`   
 6@   �� *AddressTrip+6`     5$      $ 	Provincia��6-   $		 150 6a   
 6@   �� *ProvinceTrip+6a     5%      $ País��6-   $		 100 6[   
 6;   �� *CountryTrip+6[   5(      5)      5*      � 5-   
   y�6   �� *IsDetailMode+# [  .+�5.      �� $��10q�6   .+�5/      �� $��Detalle de Orden��6   $��20(�64   $
��Bold"�6B   $��0,0,0,10q�6T   �� 50      ��6   �� *SelectedOrder.OrderId+$ ID: {0}���� 51      ��6   �� *SelectedOrder.ClientId+$ Cliente: {0}���� 52      ��6   �� *SelectedOrder.EmployeeId+$ Empleado: {0}���� 53      ��6   �� *SelectedOrder.AddressTrip+$ Dirección: {0}���� 54      ��6   �� *SelectedOrder.ProvinceTrip+$ Provincia: {0}���� 55      ��6   �� *SelectedOrder.CountryTrip+$ 
País: {0}���� 56      ��6   �� *SelectedOrder.PhoneTrip+$ Teléfono: {0}���� 57      ��6   �� *SelectedOrder.OrderDate+$ Fecha Orden: {0:d}���� 58      ��6   �� *SelectedOrder.DateTrip+$ Fecha Viaje: {0:d}���� 59      $��Detalles del Pedido:��6   $
��Bold"�68   $��0,20,0,5q�6J     5;      $ False��5=      $ False��5>      $	��150��5?      ��5;      �� *OrderDetails+ 5<      �� *SelectedOrderDetail+ 5@        5A      $ Orden ID��6-      
IsReadOnly$
 True��6[   
 6?   �� *	OrderId+6[     5B      $ Producto ID��6-   $
 True��6f   
 6B   �� *ProductDetailId+6f     5C      $ Cantidad��6-   $ False��6   
 6?   �� *Amount+ �� UpdateSourceTrigger$ PropertyChanged=�6     5D      $ Producto��6-   $
 True��6_   
 6?   �� *ProductName+6_   5F      �� 5I      $ 
Horizontal=�6   $��0,10,0,0q�66   .+�5J      �� $��Agregar Producto��6   $	��150��6]   $��5q�6i   ��68   �� *AddDetailCommand+6i   �� 5K      $��Guardar Nuevos Detalles��6   $	��180��6i   $��5q�6u   ��6?   �� *SaveNewDetailsCommand+6u   �� 5L      $��Eliminar Producto��6   $	��150��6r   $��5q�6~   ��69   �� *&$RemoveDetailFromExistingOrderCommand+6~   5M      �� 5Q      $ 
Horizontal=�6   $��0,10,0,0q�66   .+�5R      �� $��Eliminar��6   $��5q�60   $��80��6;   $��30��6F   �� 5S      $��Volver��6   $��5q�6T   $��80��6_   $��30��6j   ��6.   �� *BackToListCommand+6j   5T      5U      5V      5W      5X      