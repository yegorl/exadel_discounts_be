# exadel_discounts_be
MVP
# Docker and Ubuntu installation guide
<div class="WordSection1">

<p class="MsoNormal"><span style=""><o:p>&nbsp;</o:p></span></p>

<ol style="margin-top: 0cm;" start="1" type="1">
 <li class="MsoNormal" style=""><span style="">Проверяем </span><b style=""><i style=""><span lang="EN-US" style="">Build</span></i></b><b style=""><i style=""><span lang="EN-US" style=""> </span><span lang="EN">Windows</span></i></b><span style="">. Должен быть </span><b style=""><i style=""><span lang="EN">Build</span></i></b><b style=""><i style=""><span style=""> 18362</span></i></b><span style=""> или выше.<o:p></o:p></span></li>
</ol>

<p class="MsoNormal" style="margin-left: 36pt;"><span lang="EN" style=""><img width="117" height="145" src="README.files/image002.gif" v:shapes="image3.png"><img width="293" height="483" src="README.files/image004.gif" v:shapes="image6.png"></span></p>

<p class="MsoNormal" style="margin-left: 36pt;"><span lang="EN"><o:p>&nbsp;</o:p></span></p>

<p class="MsoNormal" style="margin-left: 36pt;"><span lang="EN" style=""><img width="579" height="435" src="README.files/image006.gif" v:shapes="image5.png"></span></p>

<p class="MsoNormal" style="margin-left: 36pt;"><span lang="EN"><o:p>&nbsp;</o:p></span></p>

<p class="MsoNormal"><span lang="EN"><o:p>&nbsp;</o:p></span></p>

<p class="MsoNormal"><span style=""><o:p>&nbsp;</o:p></span></p>

<p class="MsoNormal"><span style=""><o:p>&nbsp;</o:p></span></p>

<ol style="margin-top: 0cm;" start="2" type="1">
 <li class="MsoNormal" style=""><span style="">В </span><b style=""><i style=""><span lang="EN-US" style="">Task</span></i></b><b style=""><i style=""><span lang="EN-US" style=""> </span></i></b><b style=""><i style=""><span lang="EN-US" style="">Manager</span></i></b><span lang="EN-US" style=""> </span><b style=""><i style=""><span style="">(</span></i></b><b style=""><i style=""><span lang="EN-US" style="">Ctrl</span></i></b><b style=""><i style=""><span style="">+</span></i></b><b style=""><i style=""><span lang="EN-US" style="">Alt</span></i></b><b style=""><i style=""><span style="">+</span></i></b><b style=""><i style=""><span lang="EN-US" style="">Delete</span></i></b><b style=""><i style=""><span style="">)</span></i></b><span style=""> нужно убедится,
     что в системе включена виртуализация и если не включена, <br /> то проверить,
     поддерживает ли процессор и включить в </span><span lang="EN-US" style="">BIOS</span><span style="">.<o:p></o:p></span></li>
</ol>

<p class="MsoNormal" style="margin-left: 36pt;"><span style=""><img width="397" height="272" src="README.files/image008.jpg" v:shapes="Рисунок_x0020_9"></span><span style=""><o:p></o:p></span></p>

<p class="MsoNormal" style="margin-left: 36pt;"><span style=""><o:p>&nbsp;</o:p></span></p>

<ol style="margin-top: 0cm;" start="3" type="1">
 <li class="MsoNormal" style=""><span style="">Нажимаем сочетание клавиш <span class="SpellE"><b style=""><i style="">Win</i></b></span></span><b style=""><i style=""><span style="font-family: &quot;Cambria Math&quot;, serif;">⊞</span></i></b><b style=""><i style=""><span style=""> + R</span></i></b><span style=""> и выполняем
     команду <span class="SpellE"><b style=""><i style="">optionalfeatures</i></b></span>,
     откроется окно <span class="SpellE"><b style=""><i style="">Windows</i></b></span><b style=""><i style="">
     <span class="SpellE">Features</span></i></b>, <br /> в котором нужно включить
     компоненты <span class="SpellE"><b style=""><i style="">Virtual</i></b></span><b style=""><i style="">
     <span class="SpellE">Machine</span> <span class="SpellE">Platform</span></i></b>
     и <span class="SpellE"><b style=""><i style="">Windows</i></b></span><b style=""><i style="">
     <span class="SpellE">Subsystem</span> <span class="SpellE">for</span> <span class="SpellE">Linux</span></i></b>, после этого нужно будет перезагрузить
     машину.<o:p></o:p></span></li>
</ol>

<p class="MsoNormal" style="margin-left: 36pt;"><span style=""><o:p>&nbsp;</o:p></span></p>

<p class="MsoNormal" style="margin-left: 36pt;"><span style=""><img width="402" height="388" src="README.files/image010.gif" v:shapes="Рисунок_x0020_10"></span><span style=""><o:p></o:p></span></p>

<p class="MsoNormal" style="margin-left: 36pt;"><span style=""><o:p>&nbsp;</o:p></span></p>

<ol style="margin-top: 0cm;" start="4" type="1">
 <li class="MsoNormal" style=""><span style="">Загружаем и устанавливаем апдейт для </span><b style=""><i style=""><span lang="EN">WSL</span></i></b><b style=""><i style=""><span lang="EN" style=""> </span></i></b><b style=""><i style=""><span style="">2</span></i></b><span style=""><br>
     </span><u><span lang="EN" style="color: rgb(17, 85, 204);"><a href="https://wslstorestorage.blob.core.windows.net/wslblob/wsl_update_x64.msi"><span style="color: rgb(17, 85, 204);">https</span><span lang="RU" style="color: rgb(17, 85, 204);">://</span><span class="SpellE"><span style="color: rgb(17, 85, 204);">wslstorestorage</span></span><span lang="RU" style="color: rgb(17, 85, 204);">.</span><span style="color: rgb(17, 85, 204);">blob</span><span lang="RU" style="color: rgb(17, 85, 204);">.</span><span style="color: rgb(17, 85, 204);">core</span><span lang="RU" style="color: rgb(17, 85, 204);">.</span><span style="color: rgb(17, 85, 204);">windows</span><span lang="RU" style="color: rgb(17, 85, 204);">.</span><span style="color: rgb(17, 85, 204);">net</span><span lang="RU" style="color: rgb(17, 85, 204);">/</span><span class="SpellE"><span style="color: rgb(17, 85, 204);">wslblob</span></span><span lang="RU" style="color: rgb(17, 85, 204);">/</span><span class="SpellE"><span style="color: rgb(17, 85, 204);">wsl</span></span><span lang="RU" style="color: rgb(17, 85, 204);">_</span><span style="color: rgb(17, 85, 204);">update</span><span lang="RU" style="color: rgb(17, 85, 204);">_</span><span style="color: rgb(17, 85, 204);">x</span><span lang="RU" style="color: rgb(17, 85, 204);">64.</span><span class="SpellE"><span style="color: rgb(17, 85, 204);">msi</span></span></a></span></u><span style=""><o:p></o:p></span></li>
</ol>

<p class="MsoNormal" style="margin-left: 36pt;"><span style=""><o:p>&nbsp;</o:p></span></p>

<ol style="margin-top: 0cm;" start="5" type="1">
 <li class="MsoNormal" style=""><span style="">Открываем <span class="SpellE"><b style=""><i style="">PowerShell</i></b></span> и выполняем
     следующую команду, чтобы задать <b style=""><i style="">WSL 2</i></b>, в качестве версии по
     умолчанию при установке нового дистрибутива <span class="SpellE">Linux</span><o:p></o:p></span></li>
</ol>

<p class="MsoListParagraphCxSpFirst"><span class="SpellE"><b style=""><i style=""><span lang="EN-US" style="">wsl</span></i></b></span><b style=""><i style=""><span style=""> --</span></i></b><b style=""><i style=""><span lang="EN-US" style="">set</span></i></b><b style=""><i style=""><span style="">-</span></i></b><b style=""><i style=""><span lang="EN-US" style="">default</span></i></b><b style=""><i style=""><span style="">-</span></i></b><b style=""><i style=""><span lang="EN-US" style="">version</span></i></b><b style=""><i style=""><span style=""> 2</span></i></b><span style=""><o:p></o:p></span></p>

<p class="MsoListParagraphCxSpMiddle"><span style="">Подробнее
в руководстве от </span><b style=""><i style=""><span lang="EN-US" style="">Microsoft</span></i></b><span lang="EN-US" style=""> </span><span class="MsoHyperlink"><span style=""><a href="https://docs.microsoft.com/ru-ru/windows/wsl/install-win10">https://docs.microsoft.com/ru-ru/windows/wsl/install-win10</a></span></span><span style=""><o:p></o:p></span></p>

<p class="MsoListParagraphCxSpLast"><span style=""><o:p>&nbsp;</o:p></span></p>

<ol style="margin-top: 0cm;" start="6" type="1">
 <li class="MsoNormal" style=""><span style="">Заходим в </span><b style=""><i style=""><span lang="EN">Microsoft</span></i></b><b style=""><i style=""><span lang="EN" style=""> </span><span lang="EN">Store</span></i></b><span style=""> и устанавливаем </span><b style=""><i style=""><span lang="EN">Ubuntu</span></i></b><b style=""><i style=""><span style=""> 20.04 </span><span lang="EN">LTS</span></i></b><span style=""><o:p></o:p></span></li>
</ol>

<p class="MsoNormal" style="margin-left: 36pt;"><span lang="EN" style=""><img border="0" width="349" height="630" src="README.files/image012.gif" v:shapes="image1.png"></span></p>

<p class="MsoNormal" style="margin-left: 36pt;"><span style="">У меня </span><b style=""><i style=""><span lang="EN">Ubuntu</span></i></b><span style="">
уже установлена. <o:p></o:p></span></p>

<p class="MsoNormal" style="margin-left: 36pt;"><span lang="EN" style=""><img border="0" width="624" height="271" src="README.files/image014.gif" v:shapes="image7.png"></span></p>

<p class="MsoNormal" style="margin-left: 36pt;"><span lang="EN"><o:p>&nbsp;</o:p></span></p>

<p class="MsoNormal" style="margin-left: 36pt;"><span style="">Если у вас нет аккаунта </span><b style=""><i style=""><span lang="EN-US" style="">Microsoft</span></i></b><span lang="EN-US" style=""> </span><span style="">и/или у вас в системе не установлен </span><b style=""><i style=""><span lang="EN">Microsoft</span></i></b><b style=""><i style=""><span lang="EN" style=""> </span><span lang="EN">Store</span></i></b><span style="">, то выполнить
следующие действия в </span><b style=""><i style=""><span lang="EN">PowerShell</span></i></b><span style="">:<o:p></o:p></span></p>

<p class="MsoNormal" style="margin-left: 36pt;"><span style="">– заходим в папку, в которую будет загружен дистрибутив </span><span lang="EN">Ubuntu</span><span style=""> (примерный размер
файла 432Мб)<o:p></o:p></span></p>

<p class="MsoNormal" style="margin-left: 36pt;"><b style=""><i style=""><span lang="EN">cd</span></i></b><b style=""><i style=""><span style=""> &lt;</span><span class="SpellE"><span lang="EN">somefolder</span></span></i></b><b style=""><i style=""><span style="">&gt;<o:p></o:p></span></i></b></p>

<p class="MsoNormal" style="margin-left: 36pt;"><span style=""><o:p>&nbsp;</o:p></span></p>

<p class="MsoNormal" style="margin-left: 36pt;"><span style="">– загружаем дистрибутив </span><span lang="EN">Ubuntu</span><span style=""> 20.04<o:p></o:p></span></p>

<p class="MsoNormal" style="margin-top: 0cm; margin-right: -40.05pt; margin-bottom: 0.0001pt; margin-left: 36pt;"><b style=""><i style=""><span lang="EN">Invoke-<span class="SpellE">WebRequest</span> -Uri https://aka.ms/wslubuntu2004 -<span class="SpellE">OutFile</span> <span class="SpellE">Ubuntu.appx</span> -<span class="SpellE">UseBasicParsing</span><o:p></o:p></span></i></b></p>

<p class="MsoNormal" style="margin-left: 36pt;"><span lang="EN"><o:p>&nbsp;</o:p></span></p>

<p class="MsoNormal" style="margin-left: 36pt;"><span lang="EN">– </span><span style="">устанавливаем</span> <span style="">загруженный</span><span lang="EN"> *.appx-</span><span style="">файл</span></p>

<p class="MsoNormal" style="margin-left: 36pt;"><b style=""><i style=""><span lang="EN">Add-<span class="SpellE">AppxPackage</span> .\<span class="SpellE">Ubuntu.appx</span><o:p></o:p></span></i></b></p>

<p class="MsoNormal" style="margin-left: 36pt;"><span lang="EN"><o:p>&nbsp;</o:p></span></p>

<p class="MsoNormal" style="margin-left: 36pt;"><span style="">– для завершения установки, запускаем </span><b style=""><i style=""><span lang="EN">Ubuntu</span></i></b><span style=""> из меню пуск и добавляем новое <b style=""><i style="">Имя</i></b>
<b style=""><i style="">пользователя</i></b>
и <b style=""><i style="">Пароль</i></b>.<o:p></o:p></span></p>

<p class="MsoNormal" style="margin-left: 36pt;"><span style=""><o:p>&nbsp;</o:p></span></p>

<p class="MsoNormal" style="margin-left: 36pt;"><span style=""><img border="0" width="207" height="446" src="README.files/image016.jpg" v:shapes="Рисунок_x0020_6"></span><span style=""><o:p></o:p></span></p>

<p class="MsoNormal" style="margin-left: 36pt;"><span style=""><o:p>&nbsp;</o:p></span></p>

<p class="MsoNormal" style="margin-left: 36pt;"><span style="">Подробнее в руководстве от </span><b style=""><i style=""><span lang="EN-US" style="">Microsoft</span></i></b><span lang="EN-US" style=""> </span><span class="MsoHyperlink"><span lang="EN"><a href="https://aka.ms/wslusers">https<span lang="RU" style="">://</span>aka<span lang="RU" style="">.</span><span class="SpellE">ms</span><span lang="RU" style="">/</span><span class="SpellE">wslusers</span></a></span></span><span style=""><o:p></o:p></span></p>

<p class="MsoNormal" style="margin-left: 36pt;"><span style=""><o:p>&nbsp;</o:p></span></p>

<ol style="margin-top: 0cm;" start="7" type="1">
 <li class="MsoNormal" style=""><span style="">Загружаем, устанавливаем и запускаем <span class="SpellE"><b style=""><i style="">Docker</i></b></span><b style=""><i style="">
     <span class="SpellE">desktop</span> <span class="SpellE">for</span> <span class="SpellE">Windows</span></i></b>.<o:p></o:p></span></li>
</ol>

<p class="MsoNormal" style="margin-left: 36pt;"><span class="MsoHyperlink"><span style=""><a href="https://hub.docker.com/editions/community/docker-ce-desktop-windows">https://hub.docker.com/editions/community/docker-ce-desktop-windows</a></span></span><span style=""><o:p></o:p></span></p>

<p class="MsoNormal" align="center" style="margin-left: 36pt; text-align: center;"><span style=""><o:p>&nbsp;</o:p></span></p>

<ol style="margin-top: 0cm;" start="8" type="1">
 <li class="MsoNormal" style=""><span style="">Если в левом нижнем углу </span><b style=""><i style=""><span lang="EN">Docker</span></i></b><span style="">, индикатор зеленый – </span><b style=""><i style=""><span lang="EN">Docker</span></i></b><span style=""> готов к
     работе.<o:p></o:p></span></li>
</ol>

<p class="MsoNormal" style="margin-left: 36pt;"><span style="">(<span style="color: rgb(255, 153, 0);">некоторое время после запуска, индикатор будет
оранжевого цвета</span>, <span style="color: red;">главное, чтобы не красного</span>)<o:p></o:p></span></p>

<p class="MsoNormal"><span lang="EN" style=""><img border="0" width="624" height="361" src="README.files/image018.gif" v:shapes="image4.png"></span></p>

</div>




