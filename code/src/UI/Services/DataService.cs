﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.ObjectModel;
using System.Linq;

using Microsoft.Templates.Core;
using Microsoft.Templates.Core.Gen;
using Microsoft.Templates.UI.ViewModels.Common;
using Microsoft.Templates.UI.ViewModels.NewProject;

namespace Microsoft.Templates.UI.Services
{
    public static class DataService
    {
        public static bool LoadProjectTypes(ObservableCollection<MetadataInfoViewModel> projectTypes, string platform)
        {
            var templateProjectTypes = GenComposer.GetSupportedProjectTypes(platform);

            var data = GenContext.ToolBox.Repo.GetProjectTypes(platform)
                        .Where(m => templateProjectTypes.Contains(m.Name) && !string.IsNullOrEmpty(m.Description))
                        .Select(m => new MetadataInfoViewModel(m)).ToList();

            projectTypes.Clear();

            if (data.Any())
            {
                foreach (var projectType in data)
                {
                    projectTypes.Add(projectType);
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool LoadFrameworks(ObservableCollection<MetadataInfoViewModel> frameworks, string projectTypeName, string platform)
        {
            var templateFrameworks = GenComposer.GetSupportedFx(projectTypeName, platform);

            var targetFrameworks = GenContext.ToolBox.Repo.GetFrameworks(platform)
                                        .Where(m => templateFrameworks.Contains(m.Name))
                                        .Select(m => new MetadataInfoViewModel(m))
                                        .ToList();

            frameworks.Clear();

            foreach (var item in targetFrameworks)
            {
                frameworks.Add(item);
            }

            return frameworks.Any();
        }

        public static int LoadTemplatesGroups(ObservableCollection<ItemsGroupViewModel<TemplateInfoViewModel>> templatesGroups, TemplateType templateType, string frameworkName, string platformName)
        {
            if (!templatesGroups.Any())
            {
                var templates = GenContext.ToolBox.Repo.Get(t => t.GetTemplateType() == templateType && t.GetFrameworkList().Contains(frameworkName) && t.GetPlatform() == platformName && !t.GetIsHidden()).Select(t => new TemplateInfoViewModel(t, GenComposer.GetAllDependencies(t, frameworkName, platformName)));
                var groups = templates.GroupBy(t => t.Group).Select(gr => new ItemsGroupViewModel<TemplateInfoViewModel>(gr.Key as string, gr.ToList().OrderBy(t => t.Order))).OrderBy(gr => gr.Name);
                templatesGroups.AddRange(groups);
                return templates.Count();
            }

            return 0;
        }
    }
}
