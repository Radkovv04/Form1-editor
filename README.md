**Form1: Dynamic Note Management System**
**Form1** is a robust Windows Forms application built using the MVVM (Model-View-ViewModel) pattern. It provides a seamless note-taking experience with a focus on session persistence, categorized organization, and a visual action history.


🚀 Key Features
- Smart Save System: Automatically prompts for a unique file name upon saving.
- Duplicate Protection: Built-in logic prevents overwriting existing files by checking for naming conflicts in the database.
- Visual Session Log: A real-time history tab that logs every "Undo," "Redo," "Save," and "Load" action.
- Time-Travel Navigation: Users can click any entry in the Session Log to instantly restore the text to that specific moment in time.
- Full CRUD Support: Dedicated interfaces to Create, Read, Update, and Delete both Notes and Categories.
- Advanced Undo/Redo: Utilizes a dual-stack architecture to provide lightning-fast text recovery without heavy database overhead.

🛠️ Technical Stack
- Language: C#
- Framework: .NET / Windows Forms
- Architecture: MVVM (CommunityToolkit.Mvvm)
- Database: SQLite with Entity Framework Core
- Pattern: Memento Pattern (for state management)

📖 How It Works
- The State Engine
- The application tracks changes using two internal stacks (Undo/Redo). Unlike standard editors, NoteFlow visualizes these states in the History Tab. This is achieved by mapping text snapshots to ActionLogItems displayed via a BindingList for real-time UI synchronization.
- Database Layer
The backend utilizes Entity Framework Core to manage two primary entities:

*Notes: Stores the title, content, and category relationship.*

- Categories: Allows for logical grouping of data.

🖥️ UI Overview
The interface is designed for high productivity, featuring:
- A centralized Markdown-ready text editor.
- A Sidebar for quick category selection and session history.
- A Control Bar for primary database actions (Save, Manage, Delete).

🔧 Installation & Setup
- Clone the repository.
- Open the solution in Visual Studio 2022. (or above)
- Ensure the AppDbContext is configured for your local SQLite path.
- Build and Run.

Ensure the AppDbContext is configured for your local SQLite path.

Build and Run.
