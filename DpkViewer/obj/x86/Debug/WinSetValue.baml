   M S B A M L   `   `   `  ����D  @DpkViewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:DpkViewer	DpkViewer  5   	       DpkViewer.WinSetValue   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   'locclr-namespace:DpkViewer     5   	     � Title$'  !Выберите значение��5   	   $	��252��6#   $	��345��60    � ShowInTaskbar .   � Topmost .  � WindowStartupLocation$ CenterOwner=�6a    � WindowStyle$ 
ToolWindow=�6�    � Icon$7 1/DpkViewer;component/Resources/Other/SetValue.ico��6�    � 
ResizeMode$ NoResize=�6�    � 	Resources 5         ButtonStyle �� 
TargetType	 �� Property
 �� Value��%�   &	        �� 5   
   " ��6$   .�5      �� 	 � ��$
 50��6&   �� 5	      	 � ��$
 25��6'   �� 5
      	 � ��$
 14(�6)   �� 5      	 � ��$
 2,2,2,2q�6'   5      5       J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5           Command$. (loc:WinSetValueCommands.AcceptParametres��5      6L     5   
   -   5        $. (loc:WinSetValueCommands.CancelParametres��5      6L     5   
   -   5        $# loc:WinSetValueCommands.Clear��5      6A     5   
   -   5        $' !loc:WinSetValueCommands.Backspace��5      6E     5   
   -   5        $# loc:WinSetValueCommands.Num_0��5      6A     5   
   -   5        $# loc:WinSetValueCommands.Num_1��5      6A     5   
   -   5        $# loc:WinSetValueCommands.Num_2��5      6A     5   
   -   5        $# loc:WinSetValueCommands.Num_3��5      6A     5   
   -	   5        $# loc:WinSetValueCommands.Num_4��5      6A     5   
   -
   5        $# loc:WinSetValueCommands.Num_5��5      6A     5   
   -   5        $# loc:WinSetValueCommands.Num_6��5      6A     5   
   -   5        $# loc:WinSetValueCommands.Num_7��5      6A     5   
   -   5        $# loc:WinSetValueCommands.Num_8��5      6A     5   
   -   5        $# loc:WinSetValueCommands.Num_9��5      6A     5   
   -   5        $# loc:WinSetValueCommands.Num_A��5      6A     5    
   -   5        $# loc:WinSetValueCommands.Num_B��5       6A     5!   
   -   5        $# loc:WinSetValueCommands.Num_C��5!      6A     5"   
   -   5        $# loc:WinSetValueCommands.Num_D��5"      6A     5#   
   -   5        $# loc:WinSetValueCommands.Num_E��5#      6A     5$   
   -   5        $# loc:WinSetValueCommands.Num_F��5$      6A   .��5&      � .+�5'   
   �� -   5        $��textBlockValue��5'   4   $��27��6   $��
12,12,12,0q�6    $	��0x0��6J   $	��Top=�6U    �� TextAlignment$ Right=�6�   $��26(�6�   $��Courier New)�6�   $
��Bold"�6�   ��6m   Z� 6�   �� 5(   
   $��Ввод значения��6   $��12,45,86,49q�6*   .��5)      �  � Orientation$ 
Horizontal=�6   .+�5*      �� $#��loc:WinSetValueCommands.Num_0��6   $��0��6A   #��[  6M   �� 5+      $#��loc:WinSetValueCommands.Num_1��6   $��1��6A   #��[  6M   �� 5,      $#��loc:WinSetValueCommands.Num_2��6   $��2��6A   #��[  6M   �� 5-      $#��loc:WinSetValueCommands.Num_3��6   $��3��6A   #��[  6M   �� 5.      $#��loc:WinSetValueCommands.Num_4��6   $��4��6A   #��[  6M   �� 5/      $#��loc:WinSetValueCommands.Num_5��6   $��5��6A   #��[  6M   �� 50      $#��loc:WinSetValueCommands.Num_6��6   $��6��6A   #��[  6M   �� 51      $#��loc:WinSetValueCommands.Num_7��6   $��7��6A   #��[  6M   �� 52      $#��loc:WinSetValueCommands.Num_8��6   $��8��6A   #��[  6M   �� 53      $#��loc:WinSetValueCommands.Num_9��6   $��9��6A   #��[  6M   �� 54      $#��loc:WinSetValueCommands.Num_A��6   $��A��6A   #��[  6M   �� 55      $#��loc:WinSetValueCommands.Num_B��6   $��B��6A   #��[  6M   �� 56      $#��loc:WinSetValueCommands.Num_C��6   $��C��6A   #��[  6M   �� 57      $#��loc:WinSetValueCommands.Num_D��6   $��D��6A   #��[  6M   �� 58      $#��loc:WinSetValueCommands.Num_E��6   $��E��6A   #��[  6M   �� 59      $#��loc:WinSetValueCommands.Num_F��6   $��F��6A   #��[  6M   5:      5;      �� 5<   
   $.��(loc:WinSetValueCommands.CancelParametres��6   $��0,0,12,8q�6D   $��35��6V   $��Bottom=�6b   $��Right=�6}   .��5=      ��  �� Orientation$ 
Horizontal=�6   .+�5>      �� $5��//DpkViewer;component/Resources/Other/Cancel.ico��6   �� 5?      $��Закрыть��6   $��Center=�6*   $��Center=�6G   5@      5A      �� 5B   
   $.��(loc:WinSetValueCommands.AcceptParametres��6   $��12,0,0,8q�6D   $��35��6V   $��Bottom=�6b   $
��Left=�6}   .��5C      �� $ 
Horizontal=�6   .+�5D      �� $7��1/DpkViewer;component/Resources/Other/SetValue.ico��6   �� 5E      $��Выбрать��6   $��Center=�6*   $��Center=�6G   5F      5G      �� 5H   
   $��Операции��6   $��
0,45,12,49q�6%   $��Right=�69   $��68��6U   .��5I      � .+�5J      �� $#��loc:WinSetValueCommands.Clear��6   $��
Сброс��6A   #��[  6Q   �� 5K      $'��!loc:WinSetValueCommands.Backspace��6   $	��<--��6E   #��[  6V   5L      5M      5N      5O      