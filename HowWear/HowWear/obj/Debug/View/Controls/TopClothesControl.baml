   M S B A M L   `   `   `  ����B  >HowWear, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:HowWear.View.ControlsHowWear.View.Controls  5      5clr-namespace:HowWear.ViewModelHowWear.ViewModel  5      G(clr-namespace:HowWear.ViewModel.CommandsHowWear.ViewModel.Commands  5      -    'HowWear.View.Controls.TopClothesControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:HowWear.View.Controls     5      .vmclr-namespace:HowWear.ViewModel     5      7cm(clr-namespace:HowWear.ViewModel.Commands     5        � 	Resources  5       �� 
TargetType   colorCheckBox �� Property �� Value��%Q   &	        �� 5   
   " ��6   .�5      ��  � ��$ 2q�6'   5      5      .��5      � $y�Visible=�6    � ColumnDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      .+�5   
   �� -   5         ��Name$ ImageBtn��5      $��4��5   :   ��5   #   ��  �� Path$ VCommand�y�5      �� $ ImageVisible���5      �� 5       �� Opacity$ 0Q�6"   .��5      �� -   5         ��Name$ clothesImage��5   e   $��Uniform=�66   $��0��6H   $��0��6X   ��6   �� $ 	ImagePath�6e   5      �� 5   
   -   5        	 ��Name$	 Panel1��5   3   $��Center=�6   $��0��6C   y�5       �� $ ColorVisible�.+�5!      �� 
���  ��#��[  6(   
 d� 	IsChecked
 6O   �� $ Check[0]��� 5"      
��� ���#��[  6+   
 6R   �� $ Check[1]��� 5#      
��� ���#��[  6+   
 6R   �� $ Check[2]��� 5$      
�������#��[  6*   
 6Q   �� $ Check[3]��� 5%      
���**��#��[  6*   
 6Q   �� $ Check[4]�5&      �� 5'   
   -   5        $	 Panel2��5'   3   $��Center=�6   $��1��6C   y�5(      �� $ ColorVisible�.+�5)      �� 
��� � �#��[  6*   
 6Q   �� $ Check[5]��� 5*      
������#��[  6/   
 6V   �� $ Check[6]��� 5+      
����·�#��[  6,   
 6S   �� $ Check[7]��� 5,      
����  �#��[  6)   
 6P   �� $ Check[8]��� 5-      
����  �#��[  6)   
 6P   �� $ Check[9]�5.      �� 5/   
   -   5        $	 Panel3��5/   3   $��Center=�6   $��2��6C   y�50      �� $ ColorVisible�.+�51      �� 
���� ��#��[  6+   
 6R   �� $ 	Check[10]��� 52      
�������#��[  6.   
 6U   �� $ 	Check[11]��� 53      
�������#��[  6.   
 6U   �� $ 	Check[12]��� 54      
�������#��[  6-   
 6T   �� $ 	Check[13]��� 55      
���   �#��[  6.   
 6U   �� $ 	Check[14]�56      �� 57   
   $��3��6   $��Center=�58      $��Center=�6.   $��0q�59      $��10(�6%   ��57   !   �� $ VCommand�y�5:      �� $ ColorVisible���5;      �� 5<      $ 0Q�6"   .��5>      �� $��/Resources/accept.png��6   $��5q�63   5?      5@      5A      