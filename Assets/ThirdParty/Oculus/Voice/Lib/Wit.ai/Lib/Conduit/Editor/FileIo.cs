﻿/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * This source code is licensed under the license found in the
 * LICENSE file in the root directory of this source tree.
 */

using System;
using System.IO;

namespace Meta.Conduit
{
    internal class FileIo : IFileIo
    {
        public bool Exists(string fileName)
        {
            try
            {
                return File.Exists(fileName);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError($"FileIo Exists Check Failed\nPath: {fileName}\n{e}");
                return false;
            }
        }

        public string ReadAllText(string fileName)
        {
            try
            {
                return File.ReadAllText(fileName);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError($"FileIo ReadAllText Failed\nPath: {fileName}\n{e}");
                return string.Empty;
            }
        }

        public void WriteAllText(string path, string contents)
        {
            try
            {
                string directory = Path.GetDirectoryName(path);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                File.WriteAllText(path, contents);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError($"FileIo WriteAllText Failed\nPath: {path}\nContents:\n{contents}\n{e}");
            }
        }

        public TextReader OpenText(string fileName)
        {
            try
            {
                return File.OpenText(fileName);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError($"FileIo OpenText Failed\nPath: {fileName}\n{e}");
                return null;
            }
        }
    }
}
