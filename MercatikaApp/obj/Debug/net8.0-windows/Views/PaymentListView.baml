   M S B A M L   `   `   `  ����G  CMercatikaApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:MercatikaApp.ViewModelMercatikaApp.ViewModel  5      A%clr-namespace:MercatikaApp.ConvertersMercatikaApp.Converters  5      )    #MercatikaApp.Views.PaymentsListView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      3vm$clr-namespace:MercatikaApp.ViewModel     5      7local%clr-namespace:MercatikaApp.Converters     5      
�������  � DataContext  5      -   'MercatikaApp.ViewModel.PaymentViewModel  5	   
   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��0��6.   $��0 0 0 10q�6;   .+�5      �� $��	ID Order:��6   $��Center=�6)   $��0,0,5,0q�6D   �� 5      $��80��6   $��0,0,10,0q�6`   ��6!   �� *
SearchId+ �� UpdateSourceTrigger$ PropertyChanged=�6`   �� 5      $��State:��6   $��Center=�6&   $��0,0,5,0q�6A   �� 5      $	��120��6   $��0,0,10,0q�6e   ��6"   �� *SearchStatus+$ PropertyChanged=�6e   �� 5      $��Filter��6   $��80��6H   ��6&   �� *FilterCommand+6H   �� 5      $��Recharge��6   $��80��6P   $��5,0,0,0q�6[   ��6(   �� *LoadPaymentsCommand+6[   5      &   System.Windows.Controls.DataGrid  5    
   $��1��6      AutoGenerateColumns$ False��5#         CanUserAddRows$ False��5$         
IsReadOnly$
 True��5%      $��0,0,0,10q�5&      ��5!      �� *
Payments+ �� SelectedItem 5"      �� *SelectedPayment+   Columns 5'      0  *System.Windows.Controls.DataGridTextColumn  5(      ,  &System.Windows.Controls.DataGridColumn	   Header$	 
Payment ID��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$

 Auto 6W   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *	PaymentId+6W     5)      $	 Order ID��6%   $

 Auto 6S    67   �� *	OrderId+6S     5*      $	 Amount��6%   $

 Auto 6W    65   �� *PaymentAmount+6W     5+      $	 State��6%   $

 Auto 6O    64   �� *Estado+6O     5,      $	 Method��6%   $

 Auto 6[    65   �� *PaymentMethodName+6[     5-      $
	 Card��6%   $

 Auto 6U    63   �� *CreditCardNum+6U     5.      $	 Payment date��6%   $

 Auto 6[    6;   �� *DatePayment+6[   50      �� 53   
   $��2��6   $ 
Horizontal=�6"   $
��Left=�6;   .+�54      �� $��Pay this bill��6   $	��150��56      $��35��57      ��55      �� *PaySelectedCommand+57      58      59      5:      