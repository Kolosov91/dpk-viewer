   M S B A M L   `   `   `  ����D  @DpkViewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:DpkViewer	DpkViewer  5   	   K GListBlockViewLib, Version=1.0.0.1, Culture=neutral, PublicKeyToken=nullM8clr-namespace:ListBlockViewLib;assembly=ListBlockViewLibListBlockViewLib 5   h       DpkViewer.WinFormatView   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   'locclr-namespace:DpkViewer     5   	   Gmy8clr-namespace:ListBlockViewLib;assembly=ListBlockViewLib     5   h     � Title$5  /Форматированный просмотр��6	   $	��453��6*   $	��582��67    � WindowStartupLocation$ CenterScreen=�6C    � Icon$G A/DpkViewer;component/Resources/MainMenu/Operations/FormatView.ico��6�    � 	Resources 5         toolBarIconStyle �� 
TargetType �� Property �� Value  toolBarButtonStyle��%�   &	        &	 l     �� 5   
   " ��6)   .�5      ��  � ��$ 35��6'   �� 5	       � ��$ 35��6&   5
      �� 5   
   " ��6+   .�5      ��  � ��$ 50��6&   5      5       J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5           Command$/ )loc:FormatViewCommands.OpenDescryptorFile��5      6M     5   
   -   5        $" loc:FormatViewCommands.Clear��5      6@     5   
   -   5        $. (loc:FormatViewCommands.SaveProtolocInTxt��5      6L   .��5   
   � .+�5   
   `� 	 `� Orientation$	 
Horizontal=�6   $	��Top=�6/   .�5      c� .@�5      �� -   5        $��bt_Open��5      
 � ToolTip$Q
 KОткрыть файл с описателем формата вывода��5      $/��)loc:FormatViewCommands.OpenDescryptorFile��5      #��[ 5      .��5      �� #��[  6   $;��5/DpkViewer;component/Resources/MainMenu/File/Open.ico��5      5      �� 5      -   5        $��bt_Clear��5      $0
 *Сброс выбранного файла��5      $"��loc:FormatViewCommands.Clear��5       #��[ 5!      .��5"      �� #��[  6   $5��//DpkViewer;component/Resources/Other/Cancel.ico��5#      5$      �� 5%      �� 5&      -   5        $��txtBlock_Status��5&      $
��Wrap=�5'       �� TextAlignment$ Center=�6)   $	��150��6@   $��Courier New)�6L   $��Normal"�6e   $��12(�6y   Статус:�� 5)      �� 5*      
��� ��� �� TextDecorations$ 	Underline{�6.   .�6J   Не выбран файл6Z   5+      �� 5,      �� 5-      -   5        $��bt_Save��5-      $H
 BСохранить протокол в текстовом виде��5.      $.��(loc:FormatViewCommands.SaveProtolocInTxt��5/      #��[ 50      {�.  .��51      �� #��[  6   $;��5/DpkViewer;component/Resources/MainMenu/File/Save.ico��52      53      54      55      $  ListBlockViewLib.ListBlockView  56   
   -   5        $��listBlockView_Protocol��56   0   $��12,52,12,37q�6   60   �� 57   
   $��Bottom=�6   .@�58      �� $
��Wrap=�6   $ Right=�6,   $��Courier New)�6B   $��Normal"�6[   $��11(�6o   " Файл с описателем�� 5:      формата вывода:5<      �� 5=      �� 5>      -	   5        $)��#txtBlock_CurrentDescryptionFileName��5>      $ Center=�6C   5?      5@      5A      