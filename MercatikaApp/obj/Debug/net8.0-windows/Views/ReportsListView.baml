   M S B A M L   `   `   `  ����G  CMercatikaApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:MercatikaApp.ViewModelMercatikaApp.ViewModel  5      A%clr-namespace:MercatikaApp.ConvertersMercatikaApp.Converters  5      (    "MercatikaApp.Views.ReportsListView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      3vm$clr-namespace:MercatikaApp.ViewModel     5      7local%clr-namespace:MercatikaApp.Converters     5      
�������  � 	Resources  5         BoolToVisibilityConverter>   8MercatikaApp.Converters.InverseBoolToVisibilityConverter $  InverseBoolToVisibilityConverter  OrangeButton �� 
TargetType �� Property �� Value �� Triggers P� Property P� Value��%�  &	        &	      &	 &     �� 5	   
   6'     5
   
   61   �� 5   
   " ��6%   .�5      ��  � �� �	#FFE67C006+   �� 5       � ��
 �����6+   �� 5       � ��$ 0q�60   �� 5       � ��$
 10,5q�6(   �� 5       � ��$ Medium"�6+   �� 5       �@�Cursor$
 Hand��6'   �� 5       � �� �	#FFD269006,    5      P� 5       �@J�IsMouseOver$
 True��61   . �5      ��  � �� �	#FFD2690063   5      5      5       � DataContext 5      ,   &MercatikaApp.ViewModel.ReportViewModel  5   
   .��5       � .+�5"   
   � y�6   �� *IsDetailMode+ �� 	Converter# [ .+�5#      ]� $��5q�6   .+�5$      �� 	 �� Orientation$	 
Horizontal=�6   $	��Top=�5%      $��0,0,0,10q�5&      $��Center=�5'      .+�5(      �� $��Refresh��6   #��[ 5)      $��70��5+      $��0,0,10,0q�5,      ��5*      �� *LoadOrdersCommand+5,      �� 5.      $��
Client ID:��6   $��Center=�5/      $��0,0,5,0q�50      �� 51      $��60��6   $��0,0,10,0q�52      ��53      �� *FilterClientId+
 �� UpdateSourceTrigger$
 PropertyChanged=��� 55      $��	Province:��6   $��Center=�56      $��0,0,5,0q�57      �� 58      $	��120��6   $��0,0,10,0q�59      ��5:      �� *FilterProvince+$
 PropertyChanged=��� 5<      $��
Date From:��6   $��Center=�5=      $��0,0,5,0q�5>      (  "System.Windows.Controls.DatePicker  5?      $��0,0,10,0q�5@   !      SelectedDate 5?   !   �� *FilterDateFrom+$
 PropertyChanged=�5@   !   �� 5B      $��Date to:��6   $��Center=�5C      $��0,0,5,0q�5D        5E       6!   �� *FilterDateTo+$
 PropertyChanged=�5F      &   System.Windows.Controls.DataGrid  5H         AutoGenerateColumns$ False��5J         
IsReadOnly$
 True��5K      ��5H      �� *
OrdersView+ �� SelectedItem 5I      �� *SelectedOrder+   Columns 5L      0  *System.Windows.Controls.DataGridTextColumn  5M      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6U   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *	OrderId+6U     5N      $ Client��6-   $
 Auto 6Z    6=   �� *
ClientId+6Z     5O      $ Employee��6-   $
 Auto 6^    6?   �� *
EmployeeId+6^     5P      $ 
Order Date��6-   $
 Auto 6o    6A   �� *	OrderDate+ �� StringFormat$ d��6o     5Q      $ Address��6-   $	 300 6^    6>   �� *AddressTrip+6^     5R      $ Province��6-   $	 160 6`    6?   �� *ProvinceTrip+6`     5S      $ Country��6-   $	 160 6^    6>   �� *CountryTrip+6^   5U      5V      5W      � 5Z   
   y�6   �� *IsDetailMode+# [  .+�5[      �� $��10q�6   .+�5\      �� $��Order Detail��6   $��20(�5]      $
��Bold"�5^      $��0,0,0,10q�5_      �� 5a      ��6   �� *SelectedOrder.OrderId+$ Order ID: {0}���� 5b      ��6   �� *SelectedOrder.ClientId+$ Client ID: {0}���� 5c      ��6   �� *SelectedOrder.EmployeeId+$ Employee: {0}���� 5d      ��6   �� *SelectedOrder.AddressTrip+$ Address: {0}���� 5e      ��6   �� *SelectedOrder.ProvinceTrip+$ Province: {0}���� 5f      ��6   �� *SelectedOrder.CountryTrip+$ Country: {0}���� 5g      ��6   �� *SelectedOrder.PhoneTrip+$ Phone Number: {0}���� 5h      ��6   �� *SelectedOrder.OrderDate+$ Order Date: {0:d}���� 5i      ��6   �� *SelectedOrder.DateTrip+$ Trip Date: {0:d}���� 5k      $��Product(s) Details:��6   $
��Bold"�5l      $��0,20,0,5q�5m        5o      $ False��5q      $ False��5r      $	��150��5s      ��5o      �� *OrderDetails+ 5p      �� *SelectedOrderDetail+ 5t        5u      $ Order ID��6-      
IsReadOnly$
 True��6[    6?   �� *	OrderId+6[     5v      $ 
Product ID��6-   $
 True��6e    6A   �� *ProductDetailId+6e     5w      $ Amount��6-   $ False��6}    6=   �� *Amount+$
 PropertyChanged=�6}   5y      �� 5{      $	 
Horizontal=�6   $��0,10,0,0q�66   .+�5|      �� $��Save PDF��6   #��[ 5}      $	��150��5      $��5q�5�      ��5~      �� *SavePDFCommand+5�      �� 5�      $
��Back��6   #��[ 5�      $��80��5�      $��30��5�      $��5q�5�      ��5�      �� *BackToListCommand+5�      5�      5�      5�      5�      5�      