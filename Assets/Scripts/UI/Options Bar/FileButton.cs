using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleFileBrowser;
using FileBrowser = SimpleFileBrowser.FileBrowser;
using System;
using System.IO;

public class FileButton : DropdownContentButton
{
    public enum FileAction { New, Open, Save, SaveAs, Exit }
    public FileAction action;
    public static string GetInitialPath()
    {
        string path = null;
        if (!string.IsNullOrEmpty(Manager.FilePath))
        {
            var dir = Path.GetDirectoryName(Manager.FilePath);
            if (Directory.Exists(dir))
                path = dir;
        }
        return path;
    }

    public override void OnClick()
    {
        base.OnClick();
        if (SceneManager.GetActiveScene().name != "MainMenu")
            transform.parent.gameObject.SetActive(false);

        switch (action)
        {
            case FileAction.New:
                New();
                break;
            case FileAction.Open:
                Open();
                break;
            case FileAction.Save:
                Save();
                break;
            case FileAction.SaveAs:
                SaveAs();
                break;
            case FileAction.Exit:
                Exit();
                break;
        }
    }

    public static void New()
    {
        //Check for unsaved data, ask for save.
        Manager.FilePath = null;
        NewRoomMenu.Instance.gameObject.SetActive(true);
    }

    public static void Open()
    {
        FileBrowser.OnSuccess OnSuccess = (path) => {
            if (string.IsNullOrEmpty(path)) return;

            Manager.OpeningFile = true;
            Manager.FilePath = path;
            ImportExport.Import(path);
        };

        FileBrowser.ShowLoadDialog(OnSuccess, null, initialPath: GetInitialPath(), title: "Open");
    }

    public static void Save()
    {
        if (string.IsNullOrEmpty(Manager.FilePath))
        {
            SaveAs();
            return;
        }
        ImportExport.Export(Manager.FilePath);
    }

    public static void SaveAs()
    {

        FileBrowser.OnSuccess OnSuccess = (path) => {
            if (string.IsNullOrEmpty(path)) return;

            if (!path.Contains("."))
                path += ".room";
            Manager.FilePath = path;
            Save();
        };

        FileBrowser.ShowSaveDialog(OnSuccess, null, initialPath: GetInitialPath(), title: "Save As...");
    }

    public static void Exit()
    {
        Application.Quit(0);
    }
}
