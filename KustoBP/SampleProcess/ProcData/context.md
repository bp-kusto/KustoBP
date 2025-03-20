[`⚙️ СЦЕНАРІЇ`](./scripts.md)   

# [ДЕМО ПРОЦЕС](../readme.md) / 🌐 КОНТЕКСТ

| Властивість </br> `Property` | Тип </br> `Type` | Примітки |
| :--- | :--- | :--- |
| Процесу номер	</br> `ProcNumberStr` | Стрічка </br> `String` |
| Процесу дата </br> `ProcDate` | Дата/час </br> `DateTime` |
| 🚥 Процесу статус	</br> `ProcStatus` | ⓿ E25076j05 🎲 Статус процесу (Перечисление) </br> `E25076j05_ProcStatus` | 
| ⤵ Наша компанія </br> `OurCompany` | 📘 Наша компанія (Объект) </br> `OurCompany` | 
| ⤵ ЦФВ </br> `CFR` | 📘 ЦФВ (Центр Фінансової Відповідальності) (Объект) </br> `CFR` | 
| ⤵ Департамент	</br> `Department` | 📘 Департамент (Объект) </br> `Department` | 
| ⤵ Процесу предмет </br> `ProcSubject` | Стрічка </br> `String` |
| ✏️ Опис </br> `Description` | Стрічка </br> `String`	 |
| 💬 Примітки </br> `Remarks` | Стрічка </br> `String` |
| 📎 Вкладення з файлами </br> `AttachmentsWithFiles` | Список<Вложение (Объект)> (N-N) </br> `📦 Attachment` |  
| 📎 Вкладення з документами  </br>  `AttachmentsWithDocs` | Список<Вложение с документом (Объект)> (N-N) </br> `📦 DocumentAttachment` |
| ProcName </br> `ProcName` | Стрічка </br> `String` |
| ProcId </br> `ProcId` | Стрічка </br> `String` |
| Процесу № </br> `ProcNumber` | Ціле число </br> `Int64` | 
| 📁 Процесу робоча папка </br> `ProcWorkDir` | Папка (Объект) </br> `Folder` | 
| 📜 Процесу документ </br> `ProcDoc` | ⓿ D25076o19 🚧 SampleDoc (Документ) </br> `D25076o19_SampleDoc` | 
| 👤 Ініціатор </br> `Z10Usr_Initiator` | Пользователь (Объект) </br> `User` | 
| 👤 Економіст </br> `Z20Usr_Economist` | Пользователь (Объект)  </br> `User` | 
| 👤 Керівник </br> `Z30Usr_Chief` | 	Пользователь (Объект) </br> `User` | 
| 🛠️ ProcConfig </br> `ProcConfig` | ⓿ PC25076o02 🛠️ SampleProcess (Объект) </br> `PC25076o02_SampleProcess` | 
| 🐞 DebugStr </br> `DebugStr` | Стрічка </br> `String` | 
| 📦 УЧАСНИКИ ПРОЦЕСУ </br> `BL10_ProcMembers` | Блок </br> `P25076j_BL10_ProcMember` | 
| ░░░ ▼ </br> `BI_OrderNumb` | Ціле число </br> `Int64` | 
| ░░░ 🚥 Процесу статус </br> `BI_ProcStatus` | ⓿ E25076j05 🎲 Статус процесу (Перечисление) </br> `E25076j05_ProcStatus` | 
| ░░░ 🕜 Час </br> `BI_TimeStamp` | Дата/час </br> `DateTime` | 
| ░░░ 👤 Виконавець </br> `BI_Executor` | Пользователь (Объект) </br> `User` | 
| ░░░ 🗨 Примітки </br> `BI_Rem` | Стрічка </br> `String` | 
| ░░░ 📎 Вкладення з файлами </br> `BI_AttachmentsWithFiles` | Список<Вложение (Объект)> (N-N) </br> `📦 Attachment` | 
| ░░░ 📎 Вкладення з документами </br> `BI_AttachmentsWithDocs` | Список<Вложение с документом (Объект)> (N-N) </br> `📦 DocumentAttachment` |  