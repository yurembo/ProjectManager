using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ProjectManager
{

    public partial class ManForm : Form
    {
        string rootDir = "";
        string projDir = "";
        string projName = "";

        public ManForm()
        {
            InitializeComponent();
        }

        private void buttonCreateProject_Click(object sender, EventArgs e)
        {
            labelTotal.Visible = false;
            projDir = projectHomeDialog.SelectedPath;
            rootDir = textRootFolder.Text + '\\';
            projDir += '\\' + textProjectName.Text + '\\';
            projName = textProjectName.Text;
            if (rootDir != "" && projDir != "" && projName != "")
            {
                createT2DProject();
            }
            else MessageBox.Show("Check text fields");
        }

        private void createT2DProject()
        {
            if (Directory.Exists(projDir))
            {
                MessageBox.Show("Directory exists");
            }
            else
            {
                Directory.CreateDirectory(projDir);
                labelTotal.Text = "Project " + projDir + " was created";
                Directory.SetCurrentDirectory(projDir);
                if (copyFiles())
                    labelTotal.Visible = true;
            }
        }

        private bool copyFiles()
        {
            if (File.Exists(rootDir + Constants.exeName))
            {
                File.Copy(rootDir + Constants.exeName, projDir + Constants.exeName, true);
                File.Move(projDir + Constants.exeName, projDir + projName + ".exe");
            }
            else
            {
                notExistFile(Constants.exeName);
                return false;
            }
            if (File.Exists(rootDir + Constants.leapName))
            {
                File.Copy(rootDir + Constants.leapName, projDir + Constants.leapName, true);
            }
            else
            {
                notExistFile(Constants.leapName);
                return false;
            }
            if (File.Exists(rootDir + Constants.openALName))
            {
                File.Copy(rootDir + Constants.openALName, projDir + Constants.openALName, true);
            }
            else
            {
                notExistFile(Constants.openALName);
                return false;
            }
            /* deprecated in Torque 2D 3.3 [13.03.2016]
            if (File.Exists(rootDir + Constants.uniName))
            {
                File.Copy(rootDir + Constants.uniName, projDir + Constants.uniName, true);
            }
            else
            {
                notExistFile(Constants.uniName);
                return false;
            }
            */
            if (File.Exists(rootDir + Constants.script_main))
            {
                File.Copy(rootDir + Constants.script_main, projDir + Constants.script_main, true);
                modifyMainFile(projDir + Constants.script_main);
            }
            else
            {
                notExistFile(Constants.script_main);
                return false;
            }

            if (flag_CreateTorsionProj.Checked) createTorsionProject();
            Directory.CreateDirectory("modules");
            Directory.SetCurrentDirectory("modules");
            Directory.CreateDirectory("AppCore");
            projDir += "modules";

            DirectoryInfo di = new DirectoryInfo(Path.Combine(rootDir, "modules", "AppCore", "1"));
            FileInfo[] files = di.GetFiles();
            foreach (FileInfo file in files)
            {
                string destFile = Path.Combine(projDir, "AppCore", file.Name);
                file.CopyTo(destFile, true);
                if (file.Name == "main.cs")
                {
                    modifyProjMainFile(destFile);
                }
            }
            di = new DirectoryInfo(Path.Combine(rootDir, "modules", "AppCore", "1", "scripts"));
            files = di.GetFiles();
            Directory.CreateDirectory(Path.Combine("AppCore", "scripts"));
            foreach (FileInfo file in files)
            {
                string destFile = Path.Combine(projDir, "AppCore", "scripts", file.Name);
                file.CopyTo(destFile, true);
            }

            Directory.CreateDirectory(projName);
            createProjMainCS();
            createProjModuleTAML();
            Directory.CreateDirectory(Path.Combine(projName, "gui"));
            createProjGuiProfiles();
            Directory.CreateDirectory(Path.Combine(projName, "assets"));

            if (File.Exists(Path.Combine(rootDir, "modules\\ToyAssets\\1\\assets\\images\\tires.png")))
            {
                File.Copy(rootDir + "modules\\ToyAssets\\1\\assets\\images\\tires.png", projDir + "\\" + projName + "\\assets\\tires.png", true);
            }
            else
            {
                notExistFile("tires.png");
                return false;
            }
            if (File.Exists(Path.Combine(rootDir, "modules\\ToyAssets\\1\\assets\\images\\tires.asset.taml")))
            {
                File.Copy(rootDir + "modules\\ToyAssets\\1\\assets\\images\\tires.asset.taml", projDir + "\\" + projName + "\\assets\\tires.asset.taml", true);
            }
            else
            {
                notExistFile("tires.asset.taml");
                return false;
            }

            Directory.CreateDirectory(Path.Combine(projName, "scripts"));
            createProjScriptsControlCS();
            createProjScriptsSceneCS();
            createProjScriptsSceneWindowCS();
            createProjScriptsSpriteCS();

            return true;
        }

        private void notExistFile(string fileName)
        {
            MessageBox.Show("File is not exists " + fileName);
        }

        private async void createProjMainCS()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("function " + projName + "::create( %this )");
            sb.AppendLine("{");
            sb.AppendLine("     exec(\"./gui/guiProfiles.cs\");");
            sb.AppendLine("     exec(\"./scripts/scene.cs\");");
            sb.AppendLine("     exec(\"./scripts/scenewindow.cs\");");
            sb.AppendLine("     exec(\"./scripts/InputManager.cs\");");
            sb.AppendLine("     exec(\"./scripts/sprite.cs\");");
            sb.AppendLine("     createSceneWindow();");
            sb.AppendLine("     createScene();");
            sb.AppendLine("     mySceneWindow.setScene(myScene);");
            sb.AppendLine("     createSprite();");
            sb.AppendLine("     new ScriptObject(InputManager);");
            sb.AppendLine("     InputManager.Init_controls();");
            sb.AppendLine("}");
            sb.AppendLine("");
            sb.AppendLine("function " + projName + "::destroy( %this )");
            sb.AppendLine("{");
            sb.AppendLine("     destroySceneWindow();");
            sb.AppendLine("     InputManager.delete();");
            sb.AppendLine("}");
            using (StreamWriter outfile = new StreamWriter(Path.Combine(projDir, projName, Constants.script_main), true))
            {
                await outfile.WriteAsync(sb.ToString());
            }
        }

        private void modifyMainFile(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            if (lines.Length > 2)
            {
                for (int i = 2; i < lines.Length; ++i)
                {
                    lines[i] = lines[i].Replace("\"Torque 2D\"", '"' + projName + '"');
                    lines[i] = lines[i].Replace("\"GarageGames\"", '"' + textCompanyName.Text + '"');
                }
                File.Delete(fileName);
                File.WriteAllLines(fileName, lines);
            }
        }

        private void modifyProjMainFile(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            if (lines.Length > 0)
            {
                for (int i = 0; i < lines.Length; ++i)
                {
                    lines[i] = lines[i].Replace("Torque 2D", projName);
                    lines[i] = lines[i].Replace("loadGroup", "loadExplicit");
                    lines[i] = lines[i].Replace("gameBase", projName);
                }
                File.Delete(fileName);
                File.WriteAllLines(fileName, lines);
            }
        }

        public async void createProjModuleTAML()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<ModuleDefinition");
            sb.AppendLine("ModuleId=\"" + projName + "\"");
            sb.AppendLine("VersionId=\"1\"");
            sb.AppendLine("Description=\"" + projName + " folder\"");
            sb.AppendLine("");
            sb.AppendLine("ScriptFile=\"main.cs\"");
            sb.AppendLine("CreateFunction=\"create\"");
            sb.AppendLine("DestroyFunction=\"destroy\">");
            sb.AppendLine("");
            sb.AppendLine("<DeclaredAssets");
            sb.AppendLine("Path=\"assets\"");
            sb.AppendLine("Extension=\"asset.taml\"");
            sb.AppendLine("Recurse=\"true\"/>");
            sb.AppendLine("");
            sb.AppendLine("</ModuleDefinition>");

            using (StreamWriter outfile = new StreamWriter(Path.Combine(projDir, projName, "module.taml"), true))
            {
                await outfile.WriteAsync(sb.ToString());
            }
        }

        private async void createProjGuiProfiles()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("if(!isObject(GuiDefaultProfile)) new GuiControlProfile (GuiDefaultProfile)");
            sb.AppendLine("{");
            sb.AppendLine("Modal = true;");
            sb.AppendLine("};");
            using (StreamWriter outfile = new StreamWriter(Path.Combine(projDir, projName, "gui", "guiProfiles.cs"), true))
            {
                await outfile.WriteAsync(sb.ToString());
            }
        }

        private async void createProjScriptsControlCS()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("function InputManager::Init_controls(%this)");
            sb.AppendLine("{");
            sb.AppendLine("new ActionMap(" + projName + "Input);");
            sb.AppendLine("     " + projName + "Input.bindCmd(keyboard, \"escape\", \"quit();\", \"\");");
            sb.AppendLine("     " + projName + "Input.push();");
            sb.AppendLine("}");
            sb.AppendLine("");
            sb.AppendLine("function InputManager::destroy()");
            sb.AppendLine("{");
            sb.AppendLine("     " + projName + "Input.delete();");
            sb.AppendLine("}");
            using (StreamWriter outfile = new StreamWriter(Path.Combine(projDir, projName, "scripts", "InputManager.cs"), true))
            {
                await outfile.WriteAsync(sb.ToString());
            }
        }

        private async void createProjScriptsSceneCS()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("function createScene()");
            sb.AppendLine("{");
            sb.AppendLine("     if ( isObject(myScene) )");
            sb.AppendLine("     destroyScene();");
            sb.AppendLine("     new Scene(myScene);");
            sb.AppendLine("}");
            sb.AppendLine("");
            sb.AppendLine("function destroyScene()");
            sb.AppendLine("{");
            sb.AppendLine("     if ( !isObject(myScene) )");
            sb.AppendLine("     return;");
            sb.AppendLine("     while(myScene.getCount()) {");
            sb.AppendLine("     %obj = myScene.getObject(0);");
            sb.AppendLine("     if (isObject(%obj)) {");
            sb.AppendLine("     myScene.remove(%obj);");
            sb.AppendLine("     %obj.delete();");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("myScene.clear();");
            sb.AppendLine("myScene.delete();");
            sb.AppendLine("}");
            using (StreamWriter outfile = new StreamWriter(Path.Combine(projDir, projName, "scripts", "scene.cs"), true))
            {
                await outfile.WriteAsync(sb.ToString());
            }
        }

        private async void createProjScriptsSceneWindowCS()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("$screenWidth = 100;");
            sb.AppendLine("$screenHeight = 75;");
            sb.AppendLine("function createSceneWindow()");
            sb.AppendLine("{");
            sb.AppendLine("     if ( !isObject(mySceneWindow) )");
            sb.AppendLine("{");
            sb.AppendLine("     new SceneWindow(mySceneWindow);");
            sb.AppendLine("     mySceneWindow.Profile = GuiDefaultProfile;");
            sb.AppendLine("     Canvas.setContent(mySceneWindow);");
            sb.AppendLine("}");
            sb.AppendLine("");
            sb.AppendLine("     mySceneWindow.setCameraPosition(0,0);");
            sb.AppendLine("     mySceneWindow.setCameraSize($screenWidth, $screenHeight);");
            sb.AppendLine("     mySceneWindow.setCameraZoom(1.0);");
            sb.AppendLine("     mySceneWindow.setCameraAngle(0);");
            sb.AppendLine("     mySceneWindow.setUseObjectInputEvents(true);");
            sb.AppendLine("}");
            sb.AppendLine("");
            sb.AppendLine("function destroySceneWindow()");
            sb.AppendLine("{");
            sb.AppendLine("     if ( !isObject(mySceneWindow) )");
            sb.AppendLine("     return;");
            sb.AppendLine("     mySceneWindow.delete();");
            sb.AppendLine("}");
            using (StreamWriter outfile = new StreamWriter(Path.Combine(projDir, projName, "scripts", "scenewindow.cs"), true))
            {
                await outfile.WriteAsync(sb.ToString());
            }
        }

        private async void createTorsionProject()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<TorsionProject>");
            sb.AppendLine("<Name>" + projName + "</Name>");
            sb.AppendLine("<WorkingDir/>");
            sb.AppendLine("<EntryScript>main.cs</EntryScript>");
            sb.AppendLine("<DebugHook>dbgSetParameters( #port#, \"#password#\", true );</DebugHook>");
            sb.AppendLine("<Mods>");
            sb.AppendLine("<Folder>modules</Folder>");
            sb.AppendLine("</Mods>");
            sb.AppendLine("<ScannerExts>cs; gui</ScannerExts>");
            sb.AppendLine("<Configs>");
            sb.AppendLine("<Config>");
            sb.AppendLine("<Name>Release</Name>");
            sb.AppendLine("<Executable>" + projName + ".exe</Executable>");
            sb.AppendLine("<Arguments/>");
            sb.AppendLine("<HasExports>true</HasExports>");
            sb.AppendLine("<Precompile>false</Precompile>");
            sb.AppendLine("<InjectDebugger>true</InjectDebugger>");
            sb.AppendLine("<UseSetModPaths>false</UseSetModPaths>");
            sb.AppendLine("</Config>");
            sb.AppendLine("<Config>");
            sb.AppendLine("<Name>Debug</Name>");
            sb.AppendLine("<Executable>" + projName + "_debug.exe</Executable>");
            sb.AppendLine("<Arguments/>");
            sb.AppendLine("<HasExports>false</HasExports>");
            sb.AppendLine("<Precompile>false</Precompile>");
            sb.AppendLine("<InjectDebugger>true</InjectDebugger>");
            sb.AppendLine("<UseSetModPaths>false</UseSetModPaths>");
            sb.AppendLine("</Config>");
            sb.AppendLine("</Configs>");
            sb.AppendLine("<SearchURL/>");
            sb.AppendLine("<SearchProduct>main</SearchProduct>");
            sb.AppendLine("<SearchVersion>HEAD</SearchVersion>");
            sb.AppendLine("<ExecModifiedScripts>true</ExecModifiedScripts>");
            sb.AppendLine("</TorsionProject>");
            using (StreamWriter outfile = new StreamWriter(Path.Combine(projDir, projName + ".torsion"), true))
            {
                await outfile.WriteAsync(sb.ToString());
            }
        }

        private async void createProjScriptsSpriteCS()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("function createSprite()");
            sb.AppendLine("{");
            sb.AppendLine("     %sprite = new Sprite();");
            sb.AppendLine("     %sprite.setBodyType( static );");
            sb.AppendLine("     %sprite.Position = \"0 0\";");
            sb.AppendLine("     %sprite.Size = \"15 15\";");
            sb.AppendLine("     %sprite.SceneLayer = 31;");
            sb.AppendLine("     %sprite.Image = \"" + projName + ":tires\";");
            sb.AppendLine("     myScene.add( %sprite );");
            sb.AppendLine("}");
            using (StreamWriter outfile = new StreamWriter(Path.Combine(projDir, projName, "scripts", "sprite.cs"), true))
            {
                await outfile.WriteAsync(sb.ToString());
            }
        }

        private void ManForm_Load(object sender, EventArgs e)
        {
            buttonCreateProject.Left = ClientSize.Width / 2 - buttonCreateProject.Width / 2;
            string folder = Directory.GetCurrentDirectory();
            if (File.Exists(folder + "\\Torque2D.exe"))
                textRootFolder.Text = folder;
        }

        private void buttonSelectRootFolder_Click(object sender, EventArgs e)
        {
            if (projectRootDialog.ShowDialog() == DialogResult.OK)
            {
                textRootFolder.Text = projectRootDialog.SelectedPath;
            }
        }

        private void buttonSelectHomeFolder_Click(object sender, EventArgs e)
        {
            if (projectHomeDialog.ShowDialog() == DialogResult.OK)
            {
                textHomeFolder.Text = projectHomeDialog.SelectedPath;
            }
        }
    }

    public static class Constants
    {
        //public const string rootDir = "c:\\Torque\\Torque2D-development-3-2\\";
        public const string exeName = "Torque2D.exe";
        public const string leapName = "Leap.dll";
        public const string openALName = "OpenAL32.dll";
        public const string uniName = "unicows.dll";
        public const string script_main = "main.cs";
    }
}
