   M S B A M L   `   `   `  ����D  @DpkViewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:DpkViewer	DpkViewer  5   	       DpkViewer.WinFilter   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   'locclr-namespace:DpkViewer     5   	     � Title$-  'Параметры фильтрации��5   	   $	��300��6&   $	��235��63    � ShowInTaskbar .   � WindowStartupLocation$ CenterOwner=�6U    � WindowStyle$ 
ToolWindow=�6y    � Icon$M G/DpkViewer;component/Resources/MainMenu/Filtration/FilterParametres.ico��6�   $	��235��6�   $	��300��6�    � Topmost .  � 	Resources 5         toolBarIconStyle �� 
TargetType �� Property	 �� Value��%v   &	        �� 5   
   " ��6)   .�5      ��  � ��$	 35��6'   �� 5	       � ��$	 35��6&   5
      5      
 J� CommandBindings
 5      )  #System.Windows.Input.CommandBinding  5   
   -   5           Command$, &loc:WinFilterCommands.AcceptParametres��5      6J     5   
   -   5        $, &loc:WinFilterCommands.CancelParametres��5      6J     5   
   -   5        $. (loc:WinFilterCommands.ChooseAllAddresses��5      6L     5   
   -   5        $- 'loc:WinFilterCommands.ResetAllAddresses��5      6K   .��5      � .+�5   
   �� $,��&loc:WinFilterCommands.CancelParametres��6   $��0,0,12,8q�6B   $��35��6T   $��Bottom=�6`   $��Right=�6{   .��5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $5��//DpkViewer;component/Resources/Other/Cancel.ico��6   �� 5      $��Закрыть��6   $��Center=�6*   $��Center=�6G   5      5      �� 5   
   $,��&loc:WinFilterCommands.AcceptParametres��6   $��12,0,0,8q�6B   $��35��6T   $��Bottom=�6`   $
��Left=�6{   .��5      �� $ 
Horizontal=�6   .+�5      �� $M��G/DpkViewer;component/Resources/MainMenu/Filtration/FilterParametres.ico��6   �� 5      $��Применить��6   $��Center=�6,   $��Center=�6I   5      5      �� 5   
   $��Адреса:��6   $��12,52,12,53q�6$   .��5       �� ��. $	��Top=�63   .��5!      �� -   5        $��spAddresses��5!      5"      5#      `� 5$   
   $��0,3,0,0q�6    `� Orientation$ 
Horizontal=�6'   $	��Top=�6@   .�5%      c� .@�5&      �� $.��(loc:WinFilterCommands.ChooseAllAddresses��6    � ToolTip$ Выбрать всё��6M   $
��Left=�6c   $	��Top=�6~   .��5'      �� $ 
Horizontal=�6!   .+�5(      �� #��[  6    $8��2/DpkViewer;component/Resources/Other/ChooseAll.ico��6J   5)      5*      �� 5+      $-��'loc:WinFilterCommands.ResetAllAddresses��6   $ Сбросить всё��6L   $
��Left=�6c   $	��Top=�6~   .��5,      �� $ 
Horizontal=�6!   .+�5-      �� #��[  6    $7��1/DpkViewer;component/Resources/Other/ClearAll.ico��6J   5.      5/      50      51      52      53      