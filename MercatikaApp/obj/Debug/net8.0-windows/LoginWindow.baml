   M S B A M L   `   `   `  ����G  CMercatikaApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:MercatikaApp.ViewModelsMercatikaApp.ViewModels  5   	   $    MercatikaApp.Views.LoginWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   4vm%clr-namespace:MercatikaApp.ViewModels     5   	     � Title$  Login��5   	   $	��350��5   	   $	��420��6    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�5   	   
������� � DataContext 5      ,   &MercatikaApp.ViewModels.LoginViewModel  5   
    � 	Resources 5      ��%    5      .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5"   
   �� $��Resources/LogoMK.png��6   $��0��5#      $��2��6   $��80��5$      $��Center=�5%      $��0,0,0,20q�5&      �� 5)   
   $��	Username:��6   $��1��6%   $��0��62   $��Center=�5*      $��	0,0,10,10q�6/   $��14(�5+      $
��Bold"�6"   �� 5,   
   $��1��6   $��1��6   $��0,0,0,10q�5-      $��28��6$   ��5.      �� *
Username+ �� UpdateSourceTrigger$ PropertyChanged=��� 51   
   $��	Password:��6   $��2��6%   $��0��62   $��Center=�52      $��	0,0,10,10q�6/   $��14(�53      $
��Bold"�6"   G� 54   
   -   5         G�Name$ pwdBox��54      $��2��55      $��1��6#   $��0,0,0,10q�56      $��28��6(   57      �� 5:   
   $��3��6   $��2��6"   	 �� Orientation$	 
Horizontal=�5;      $��Right=�5<      $��0,10,0,0q�5=      .+�5>      �� $��Login��6      OrangeButton#��[  5?      ��5@      �� *LoginCommand+{�5A      �� 
 �� Path$
 IsBusy� $  InverseBoolToVisibilityConverter �� 	Converter# [ �� 5B      $��Cancel��6   #��[  5C      $��10,0,0,0q�5E      ��5D      �� *CancelCommand+5E      5F      �� 5I   
   $��4��6   $��2��6!   
���  ��$
��Bold"�5L      $
��Wrap=�5M      $��0,10,0,0q�5N      $��Center=�5O      ��5J      �� *ErrorMessage+5O      5P      5Q      