   M S B A M L   `   `   `  ����G  CMercatikaApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:MercatikaApp.ViewModelMercatikaApp.ViewModel  5   	   *    $MercatikaApp.Views.ProductInsertView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   3vm$clr-namespace:MercatikaApp.ViewModel     5   	     � Title$  Insertar Producto��5   	   $	��300��6#   $	��450��60    � WindowStartupLocation$ CenterScreen=�5   	   
������� � DataContext 5	      -   'MercatikaApp.ViewModel.ProductViewModel  5
   
    � 	Resources 5         OrangeButton �� 
TargetType �� Property �� Value �� Triggers P� Property	 P� Value  
LabelStyle��%(  &	        &	 �    '�� ~    '�� ?    �� 5   
   " ��6%   .�5      ��  � �� �	#FFE67C006+   �� 5       � ��
 �����6+   �� 5       � ��$ 0q�60   �� 5       � ��$
 10,5q�6(   �� 5       � ��$ Medium"�6+   �� 5       �@�Cursor$
 Hand��6'   �� 5       � �� �	#FFD269006,    5      P� 5       �@J�IsMouseOver$
	 True��61   . �5      ��  � �� �	#FFD2690063   5      5      �� 5!   
   " ��6#   .�5"      ��  � ��$ 14(�6)   �� 5#       � ��$ 0,5,0,2q�6'   �� 5$       � �� �	#FF1F1F1F6+   �� 5%       � ��$ SemiBold"�6+   5&      �� 5)   
   " ��6   .�5*      ��  � ��$ 0,0,0,10q�6'   �� 5+       � ��$ 5q�6(   �� 5,       � ��$ 13(�6)   �� 5-       � �� �	#FFCCCCCC6,   5.      �� 51   
   " ��6   .�52      ��  � ��$ 0,0,0,10q�6'   �� 53       � ��$ 5q�6(   �� 54       � ��$ 13(�6)   �� 55       � �� �	#FFCCCCCC6,   56      57      .��59      � $��20q�6   
 � RowDefinitions
 5:   
   �� 5;      $��* �6   �� 5<      $
��Auto �6   .+�5?   
   �� $��0��6   $��0 0 0 10q�6"   .+�5@      �� $��Product name��6   #��[ 6,   �� 5A      ��6   �� *SelectedProduct.ProductName+ �� UpdateSourceTrigger$ PropertyChanged=��� 5C      $��Price��6   #��[ 6%   �� 5D      ��6   �� *SelectedProduct.Price+$ PropertyChanged=��� 5F      $��Category��6   #��[ 6(   �� 5G       �� DisplayMemberPath$ Description��5H      $	��200��5J      $��25��6#   ��5G      �� *
Categories+ �� SelectedItem 5I      �� *SelectedProduct.CategoryCode+ �� Mode$ TwoWay=�5J   #   5K      �� 5M   
   $��1��6    �� Orientation$ 
Horizontal=�6"   $��Right=�6;   .+�5N      �� $��Insert��6   $	��100��5O      #��[  5P      ��5Q      �� *
AddCommand+5R      5S      5T      