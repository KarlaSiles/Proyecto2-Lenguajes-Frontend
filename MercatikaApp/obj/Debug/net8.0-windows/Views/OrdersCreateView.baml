   M S B A M L   `   `   `  ����G  CMercatikaApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:MercatikaApp.ViewModelMercatikaApp.ViewModel  5      )    #MercatikaApp.Views.OrdersCreateView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      3vm$clr-namespace:MercatikaApp.ViewModel     5      
�������  � DataContext  5      +   %MercatikaApp.ViewModel.OrderViewModel  5   
   .��5      �� .��5   
   �� $��20q�6   .+�5      �� $��Crear Nueva Orden��6   $��20(�61   $
��Bold"�6?   $��0,0,0,20q�6Q   �� 5       �� Orientation$ 
Horizontal=�6   $	��0,5q�62   .+�5      �� $��Cliente ID:��6   $	��100��6/   $��Center=�6;   �� 5      $	��100��6b    �� 
IsReadOnly$
 True��6n   ��6   �� *NewOrder.ClientId+ �� UpdateSourceTrigger$ PropertyChanged=�6n   �� 5      $��Seleccionar��6   $	��100��6/   $��10,0,0,0q�6;   ��5      �� *SelectClientCommand+5      �� 5      $ 
Horizontal=�6   $	��0,5q�62   .+�5      �� $��Empleado ID:��6   $	��100��60   $��Center=�6<   �� 5      $	��200��6d   ��6   �� *NewOrder.EmployeeId+$ PropertyChanged=�6d   5      �� 5      $ 
Horizontal=�6   $	��0,5q�62   .+�5      �� $��Dirección:��6   $	��100��6.   $��Center=�6:   �� 5       $	��200��6e   ��6   �� *NewOrder.AddressTrip+$ PropertyChanged=�6e   5!      �� 5#      $ 
Horizontal=�6   $	��0,5q�62   .+�5$      �� $��
Provincia:��6   $	��100��6.   $��Center=�6:   �� 5%      $	��200��6f   ��6   �� *NewOrder.ProvinceTrip+$ PropertyChanged=�6f   5&      �� 5(      $ 
Horizontal=�6   $	��0,5q�62   .+�5)      �� $��País:��6   $	��100��6)   $��Center=�65   �� 5*      $	��200��6e   ��6   �� *NewOrder.CountryTrip+$ PropertyChanged=�6e   5+      �� 5-      $ 
Horizontal=�6   $	��0,5q�62   .+�5.      �� $��
Teléfono:��6   $	��100��6-   $��Center=�69   �� 5/      $	��200��6c   ��6   �� *NewOrder.PhoneTrip+$ PropertyChanged=�6c   50      �� 52      $ 
Horizontal=�6   $	��0,5q�62   .+�53      �� $��Fecha Orden:��6   $	��100��60   $��Center=�6<   (  "System.Windows.Controls.DatePicker  54      $	��200��6n      SelectedDate 6   �� *NewOrder.OrderDate+$ PropertyChanged=�6n   55      �� 57      $ 
Horizontal=�6   $	��0,5q�62   .+�58      �� $��Fecha Viaje:��6   $	��100��60   $��Center=�6<     59      $	��200��6m    6   �� *NewOrder.DateTrip+$ PropertyChanged=�6m   5:      �� 5=      $��Detalles del Pedido��6   $
��Bold"�63   $��	0,20,0,10q�6E   &   System.Windows.Controls.DataGrid  5?         AutoGenerateColumns$ False��6@      CanUserAddRows$
 True��6\   $	��200��6r   ��6   �� *NewOrder.Details+   Columns 5@      0  *System.Windows.Controls.DataGridTextColumn  5A      ,  &System.Windows.Controls.DataGridColumn   Header$ Producto ID��6)   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6>   �� *ProductDetailId+$ PropertyChanged=�  5B      $ Cantidad��6)   	 6;   �� *Amount+$ PropertyChanged=�  5C      $ Producto��6)   
   
IsReadOnly$

 True��6[   	 6;   �� *ProductName+6[   5E      �� 5G      $��Agregar Producto��6   $	��150��60   $��0,10,0,0q�6<   ��5H      �� *AddProductToOrderCommand+�� 5K      $ 
Horizontal=�6   $��0,20,0,0q�62   .+�5L      �� -   5        $��Guardar��5L      $	��100��6F   $��5q�6R   �� 5M      $��Cancelar��6   $	��100��6R   $��5q�6^   ��6,   �� *BackToListCommand+6^   5N      5O      5P      5Q      