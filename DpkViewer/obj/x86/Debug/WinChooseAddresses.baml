   M S B A M L   `   `   `  ����D  @DpkViewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:DpkViewer	DpkViewer  5   	   "    DpkViewer.WinChooseAddresses   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   'locclr-namespace:DpkViewer     5   	     � Title$  Выбор адресов��5   	   $	��300��6   $	��225��6,    � ShowInTaskbar .   � Topmost .  � WindowStartupLocation$ CenterOwner=�6]    � WindowStyle$ 
ToolWindow=�6�   $	��225��6�   $	��300��6�    � 	Resources 5         toolBarIconStyle �� 
TargetType �� Property �� Value��%v   &	        �� 5   
   " ��6)   .�5      ��  � ��$ 35��6'   �� 5	       � ��$ 35��6&   5
      5      	 J� CommandBindings	 5      )  #System.Windows.Input.CommandBinding  5   
   -   5        
   Command$5
 /loc:WinChooseAddressesCommands.AcceptParametres��5      6S     5   
   -   5        $5
 /loc:WinChooseAddressesCommands.CancelParametres��5      6S     5   
   -   5        $7
 1loc:WinChooseAddressesCommands.ChooseAllAddresses��5      6U     5   
   -   5        $6
 0loc:WinChooseAddressesCommands.ResetAllAddresses��5      6T   .��5      � .+�5   
   `� $��0,3,0,0q�6    `� Orientation$ 
Horizontal=�6'   $	��Top=�6@   .�5      c� .@�5      �� $7��1loc:WinChooseAddressesCommands.ChooseAllAddresses��6    � ToolTip$ Выбрать всё��6V   $
��Left=�6l   $	��Top=�6�   .��5      ��  �� Orientation$ 
Horizontal=�6!   .+�5      �� #��[  6    $8��2/DpkViewer;component/Resources/Other/ChooseAll.ico��6J   5      5      �� 5      $6��0loc:WinChooseAddressesCommands.ResetAllAddresses��6   $ Сбросить всё��6T   $
��Left=�6k   $	��Top=�6�   .��5      �� $ 
Horizontal=�6!   .+�5      �� #��[  6    $7��1/DpkViewer;component/Resources/Other/ClearAll.ico��6J   5      5      5      5       �� 5!   
   $��Адреса:��6   $��12,52,12,53q�6$   .��5"      �� ��. $	��Top=�63   .��5#      �� -   5        $��spAddresses��5#      5$      5%      �� 5&   
   $5��/loc:WinChooseAddressesCommands.CancelParametres��6   $��0,0,12,8q�6K   $��35��6]   $��Bottom=�6i   $��Right=�6�   .��5'      �� $ 
Horizontal=�6   .+�5(      �� $5��//DpkViewer;component/Resources/Other/Cancel.ico��6   �� 5)      $��Закрыть��6   $��Center=�6*   $��Center=�6G   5*      5+      �� 5,   
   $5��/loc:WinChooseAddressesCommands.AcceptParametres��6   $��12,0,0,8q�6K   $��35��6]   $��Bottom=�6i   $
��Left=�6�   .��5-      �� $ 
Horizontal=�6   .+�5.      �� $7��1/DpkViewer;component/Resources/Other/SetValue.ico��6   �� 5/      $��Выбрать��6   $��Center=�6*   $��Center=�6G   50      51      52      53      