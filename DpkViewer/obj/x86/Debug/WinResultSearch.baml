   M S B A M L   `   `   `  ����D  @DpkViewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:DpkViewer	DpkViewer  5   	   L HListStringViewWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullP:clr-namespace:ListStringViewWPF;assembly=ListStringViewWPFListStringViewWPF 5   �       DpkViewer.WinResultSearch   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   'locclr-namespace:DpkViewer     5   	   Imy:clr-namespace:ListStringViewWPF;assembly=ListStringViewWPF     5   �     � Title$'  !Результаты поиска��6	   $	��431��6#   $	��533��60    � Icon$? 9/DpkViewer;component/Resources/MainMenu/Search/Search.ico��6<    � Topmost .  � WindowStyle$ 
ToolWindow=�6�    � WindowStartupLocation$ CenterOwner=�6�    J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5           Command$( "loc:WinResultSearchCommands.Cancel��5      6F     5   
   -   5        $&  loc:WinResultSearchCommands.Next��5      6D     5	   
   -   5        $* $loc:WinResultSearchCommands.Previous��5	      6H   .��5      � .+�5   
   �� -   5        $��labelResultAddresses��5   K   $$��Адрес: не задано;��6   $��28��6,   $��	9,12,12,0q�68   $	��Top=�6g   &   ListStringViewWPF.ListStringView  5   
   -   5        $��listStringViewResult��5   /   $��	9,80,6,47q�6   6/   �� 5   
   $(��"loc:WinResultSearchCommands.Cancel��6   $��0,0,6,6q�6>   $��35��6O   $��Bottom=�6[   $��Right=�6v   $��85��6�   .��5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $5��//DpkViewer;component/Resources/Other/Cancel.ico��6   �� 5      $��Закрыть��6   $��Center=�6*   $��Center=�6G   5      5      �� 5   
   $&�� loc:WinResultSearchCommands.Next��6   $��99,0,0,6q�6<   $��35��6N   $��Bottom=�6Z   $
��Left=�6u   $��85��6�   .��5      �� $ 
Horizontal=�6   .+�5      �� $3��-/DpkViewer;component/Resources/Other/Next.ico��6   �� 5      $��	След.��6   $��Center=�6(   $��Center=�6E   5      5      �� 5   
   $*��$loc:WinResultSearchCommands.Previous��6   $��9,0,0,6q�6@   $��35��6Q   $��Bottom=�6]   $
��Left=�6x   $��85��6�   .��5      �� $ 
Horizontal=�6   .+�5      �� $7��1/DpkViewer;component/Resources/Other/Previous.ico��6   �� 5      $��	Пред.��6   $��Center=�6(   $��Center=�6E   5      5      �� 5    
   $��23��6   $
��Left=�6    $��	12,46,0,0q�6;   $��Данные:��6N   $	��Top=�6]   �� 5!   
   -   5        $��textBlock_Data��5!   4   $��23��6   $��
61,46,12,0q�6    $��	TextBlock��6J   $	��Top=�6[   $��Courier New)�6s   $��14(�6�   $
��Bold"�6�    �� TextAlignment$
 Left=�6�   6�   5"      5#      