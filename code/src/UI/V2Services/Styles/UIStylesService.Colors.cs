﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Windows;
using System.Windows.Media;

namespace Microsoft.Templates.UI.V2Services
{
    public partial class UIStylesService
    {
        public static readonly DependencyProperty WindowPanelProperty = DependencyProperty.Register("WindowPanel", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush WindowPanel
        {
            get { return (Brush)GetValue(WindowPanelProperty); }
            set { SetValue(WindowPanelProperty, value); }
        }

        public static readonly DependencyProperty WindowBorderProperty = DependencyProperty.Register("WindowBorder", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush WindowBorder
        {
            get { return (Brush)GetValue(WindowBorderProperty); }
            set { SetValue(WindowBorderProperty, value); }
        }

        public static readonly DependencyProperty HeaderTextProperty = DependencyProperty.Register("HeaderText", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush HeaderText
        {
            get { return (Brush)GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, value); }
        }

        public static readonly DependencyProperty HeaderTextSecondaryProperty = DependencyProperty.Register("HeaderTextSecondary", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush HeaderTextSecondary
        {
            get { return (Brush)GetValue(HeaderTextSecondaryProperty); }
            set { SetValue(HeaderTextSecondaryProperty, value); }
        }

        public static readonly DependencyProperty HyperlinkProperty = DependencyProperty.Register("Hyperlink", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush Hyperlink
        {
            get { return (Brush)GetValue(HyperlinkProperty); }
            set { SetValue(HyperlinkProperty, value); }
        }

        public static readonly DependencyProperty HyperlinkHoverProperty = DependencyProperty.Register("HyperlinkHover", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush HyperlinkHover
        {
            get { return (Brush)GetValue(HyperlinkHoverProperty); }
            set { SetValue(HyperlinkHoverProperty, value); }
        }

        public static readonly DependencyProperty HyperlinkPressedProperty = DependencyProperty.Register("HyperlinkPressed", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush HyperlinkPressed
        {
            get { return (Brush)GetValue(HyperlinkPressedProperty); }
            set { SetValue(HyperlinkPressedProperty, value); }
        }

        public static readonly DependencyProperty HyperlinkDisabledProperty = DependencyProperty.Register("HyperlinkDisabled", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush HyperlinkDisabled
        {
            get { return (Brush)GetValue(HyperlinkDisabledProperty); }
            set { SetValue(HyperlinkDisabledProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemActiveProperty = DependencyProperty.Register("SelectedItemActive", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush SelectedItemActive
        {
            get { return (Brush)GetValue(SelectedItemActiveProperty); }
            set { SetValue(SelectedItemActiveProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemInactiveProperty = DependencyProperty.Register("SelectedItemInactive", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush SelectedItemInactive
        {
            get { return (Brush)GetValue(SelectedItemInactiveProperty); }
            set { SetValue(SelectedItemInactiveProperty, value); }
        }

        public static readonly DependencyProperty ListItemMouseOverProperty = DependencyProperty.Register("ListItemMouseOver", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush ListItemMouseOver
        {
            get { return (Brush)GetValue(ListItemMouseOverProperty); }
            set { SetValue(ListItemMouseOverProperty, value); }
        }

        public static readonly DependencyProperty ListItemDisabledTextProperty = DependencyProperty.Register("ListItemDisabledText", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush ListItemDisabledText
        {
            get { return (Brush)GetValue(ListItemDisabledTextProperty); }
            set { SetValue(ListItemDisabledTextProperty, value); }
        }

        public static readonly DependencyProperty GridHeadingBackgroundProperty = DependencyProperty.Register("GridHeadingBackground", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush GridHeadingBackground
        {
            get { return (Brush)GetValue(GridHeadingBackgroundProperty); }
            set { SetValue(GridHeadingBackgroundProperty, value); }
        }

        public static readonly DependencyProperty GridHeadingHoverBackgroundProperty = DependencyProperty.Register("GridHeadingHoverBackground", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush GridHeadingHoverBackground
        {
            get { return (Brush)GetValue(GridHeadingHoverBackgroundProperty); }
            set { SetValue(GridHeadingHoverBackgroundProperty, value); }
        }

        public static readonly DependencyProperty GridHeadingTextProperty = DependencyProperty.Register("GridHeadingText", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush GridHeadingText
        {
            get { return (Brush)GetValue(GridHeadingTextProperty); }
            set { SetValue(GridHeadingTextProperty, value); }
        }

        public static readonly DependencyProperty GridHeadingHoverTextProperty = DependencyProperty.Register("GridHeadingHoverText", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush GridHeadingHoverText
        {
            get { return (Brush)GetValue(GridHeadingHoverTextProperty); }
            set { SetValue(GridHeadingHoverTextProperty, value); }
        }

        public static readonly DependencyProperty GridLineProperty = DependencyProperty.Register("GridLine", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush GridLine
        {
            get { return (Brush)GetValue(GridLineProperty); }
            set { SetValue(GridLineProperty, value); }
        }

        public static readonly DependencyProperty SectionDividerProperty = DependencyProperty.Register("SectionDivider", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush SectionDivider
        {
            get { return (Brush)GetValue(SectionDividerProperty); }
            set { SetValue(SectionDividerProperty, value); }
        }

        public static readonly DependencyProperty WindowButtonProperty = DependencyProperty.Register("WindowButton", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush WindowButton
        {
            get { return (Brush)GetValue(WindowButtonProperty); }
            set { SetValue(WindowButtonProperty, value); }
        }

        public static readonly DependencyProperty WindowButtonHoverProperty = DependencyProperty.Register("WindowButtonHover", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush WindowButtonHover
        {
            get { return (Brush)GetValue(WindowButtonHoverProperty); }
            set { SetValue(WindowButtonHoverProperty, value); }
        }

        public static readonly DependencyProperty WindowButtonDownProperty = DependencyProperty.Register("WindowButtonDown", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush WindowButtonDown
        {
            get { return (Brush)GetValue(WindowButtonDownProperty); }
            set { SetValue(WindowButtonDownProperty, value); }
        }

        public static readonly DependencyProperty WindowButtonBorderProperty = DependencyProperty.Register("WindowButtonBorder", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush WindowButtonBorder
        {
            get { return (Brush)GetValue(WindowButtonBorderProperty); }
            set { SetValue(WindowButtonBorderProperty, value); }
        }

        public static readonly DependencyProperty WindowButtonHoverBorderProperty = DependencyProperty.Register("WindowButtonHoverBorder", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush WindowButtonHoverBorder
        {
            get { return (Brush)GetValue(WindowButtonHoverBorderProperty); }
            set { SetValue(WindowButtonHoverBorderProperty, value); }
        }

        public static readonly DependencyProperty WindowButtonDownBorderProperty = DependencyProperty.Register("WindowButtonDownBorder", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush WindowButtonDownBorder
        {
            get { return (Brush)GetValue(WindowButtonDownBorderProperty); }
            set { SetValue(WindowButtonDownBorderProperty, value); }
        }

        public static readonly DependencyProperty WindowButtonGlyphProperty = DependencyProperty.Register("WindowButtonGlyph", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush WindowButtonGlyph
        {
            get { return (Brush)GetValue(WindowButtonGlyphProperty); }
            set { SetValue(WindowButtonGlyphProperty, value); }
        }

        public static readonly DependencyProperty WindowButtonHoverGlyphProperty = DependencyProperty.Register("WindowButtonHoverGlyph", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush WindowButtonHoverGlyph
        {
            get { return (Brush)GetValue(WindowButtonHoverGlyphProperty); }
            set { SetValue(WindowButtonHoverGlyphProperty, value); }
        }

        public static readonly DependencyProperty WindowButtonDownGlyphProperty = DependencyProperty.Register("WindowButtonDownGlyph", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush WindowButtonDownGlyph
        {
            get { return (Brush)GetValue(WindowButtonDownGlyphProperty); }
            set { SetValue(WindowButtonDownGlyphProperty, value); }
        }

        public static readonly DependencyProperty WizardFooterProperty = DependencyProperty.Register("WizardFooter", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush WizardFooter
        {
            get { return (Brush)GetValue(WizardFooterProperty); }
            set { SetValue(WizardFooterProperty, value); }
        }

        public static readonly DependencyProperty WizardFooterTextProperty = DependencyProperty.Register("WizardFooterText", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush WizardFooterText
        {
            get { return (Brush)GetValue(WizardFooterTextProperty); }
            set { SetValue(WizardFooterTextProperty, value); }
        }

        public static readonly DependencyProperty CardTitleTextProperty = DependencyProperty.Register("CardTitleText", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush CardTitleText
        {
            get { return (Brush)GetValue(CardTitleTextProperty); }
            set { SetValue(CardTitleTextProperty, value); }
        }

        public static readonly DependencyProperty CardDescriptionTextProperty = DependencyProperty.Register("CardDescriptionText", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush CardDescriptionText
        {
            get { return (Brush)GetValue(CardDescriptionTextProperty); }
            set { SetValue(CardDescriptionTextProperty, value); }
        }

        public static readonly DependencyProperty CardBackgroundDefaultProperty = DependencyProperty.Register("CardBackgroundDefault", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush CardBackgroundDefault
        {
            get { return (Brush)GetValue(CardBackgroundDefaultProperty); }
            set { SetValue(CardBackgroundDefaultProperty, value); }
        }

        public static readonly DependencyProperty CardBackgroundFocusProperty = DependencyProperty.Register("CardBackgroundFocus", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush CardBackgroundFocus
        {
            get { return (Brush)GetValue(CardBackgroundFocusProperty); }
            set { SetValue(CardBackgroundFocusProperty, value); }
        }

        public static readonly DependencyProperty CardBackgroundHoverProperty = DependencyProperty.Register("CardBackgroundHover", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush CardBackgroundHover
        {
            get { return (Brush)GetValue(CardBackgroundHoverProperty); }
            set { SetValue(CardBackgroundHoverProperty, value); }
        }

        public static readonly DependencyProperty CardBackgroundPressedProperty = DependencyProperty.Register("CardBackgroundPressed", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush CardBackgroundPressed
        {
            get { return (Brush)GetValue(CardBackgroundPressedProperty); }
            set { SetValue(CardBackgroundPressedProperty, value); }
        }

        public static readonly DependencyProperty CardBackgroundSelectedProperty = DependencyProperty.Register("CardBackgroundSelected", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush CardBackgroundSelected
        {
            get { return (Brush)GetValue(CardBackgroundSelectedProperty); }
            set { SetValue(CardBackgroundSelectedProperty, value); }
        }

        public static readonly DependencyProperty CardBackgroundDisabledProperty = DependencyProperty.Register("CardBackgroundDisabled", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush CardBackgroundDisabled
        {
            get { return (Brush)GetValue(CardBackgroundDisabledProperty); }
            set { SetValue(CardBackgroundDisabledProperty, value); }
        }

        public static readonly DependencyProperty CardBorderDefaultProperty = DependencyProperty.Register("CardBorderDefault", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush CardBorderDefault
        {
            get { return (Brush)GetValue(CardBorderDefaultProperty); }
            set { SetValue(CardBorderDefaultProperty, value); }
        }

        public static readonly DependencyProperty CardBorderFocusProperty = DependencyProperty.Register("CardBorderFocus", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush CardBorderFocus
        {
            get { return (Brush)GetValue(CardBorderFocusProperty); }
            set { SetValue(CardBorderFocusProperty, value); }
        }

        public static readonly DependencyProperty CardBorderHoverProperty = DependencyProperty.Register("CardBorderHover", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush CardBorderHover
        {
            get { return (Brush)GetValue(CardBorderHoverProperty); }
            set { SetValue(CardBorderHoverProperty, value); }
        }

        public static readonly DependencyProperty CardBorderPressedProperty = DependencyProperty.Register("CardBorderPressed", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush CardBorderPressed
        {
            get { return (Brush)GetValue(CardBorderPressedProperty); }
            set { SetValue(CardBorderPressedProperty, value); }
        }

        public static readonly DependencyProperty CardBorderSelectedProperty = DependencyProperty.Register("CardBorderSelected", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush CardBorderSelected
        {
            get { return (Brush)GetValue(CardBorderSelectedProperty); }
            set { SetValue(CardBorderSelectedProperty, value); }
        }

        public static readonly DependencyProperty CardBorderDisabledProperty = DependencyProperty.Register("CardBorderDisabled", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush CardBorderDisabled
        {
            get { return (Brush)GetValue(CardBorderDisabledProperty); }
            set { SetValue(CardBorderDisabledProperty, value); }
        }

        public static readonly DependencyProperty ListItemTextProperty = DependencyProperty.Register("ListItemText", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush ListItemText
        {
            get { return (Brush)GetValue(ListItemTextProperty); }
            set { SetValue(ListItemTextProperty, value); }
        }

        public static readonly DependencyProperty ListItemTextDisabledProperty = DependencyProperty.Register("ListItemTextDisabled", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush ListItemTextDisabled
        {
            get { return (Brush)GetValue(ListItemTextDisabledProperty); }
            set { SetValue(ListItemTextDisabledProperty, value); }
        }

        public static readonly DependencyProperty ButtonBorderProperty = DependencyProperty.Register("ButtonBorder", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush ButtonBorder
        {
            get { return (Brush)GetValue(ButtonBorderProperty); }
            set { SetValue(ButtonBorderProperty, value); }
        }

        public static readonly DependencyProperty ButtonProperty = DependencyProperty.Register("Button", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush Button
        {
            get { return (Brush)GetValue(ButtonProperty); }
            set { SetValue(ButtonProperty, value); }
        }

        public static readonly DependencyProperty ButtonBorderDefaultProperty = DependencyProperty.Register("ButtonBorderDefault", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush ButtonBorderDefault
        {
            get { return (Brush)GetValue(ButtonBorderDefaultProperty); }
            set { SetValue(ButtonBorderDefaultProperty, value); }
        }

        public static readonly DependencyProperty ButtonBorderFocusedProperty = DependencyProperty.Register("ButtonBorderFocused", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush ButtonBorderFocused
        {
            get { return (Brush)GetValue(ButtonBorderFocusedProperty); }
            set { SetValue(ButtonBorderFocusedProperty, value); }
        }

        public static readonly DependencyProperty ButtonBorderHoverProperty = DependencyProperty.Register("ButtonBorderHover", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush ButtonBorderHover
        {
            get { return (Brush)GetValue(ButtonBorderHoverProperty); }
            set { SetValue(ButtonBorderHoverProperty, value); }
        }

        public static readonly DependencyProperty ButtonBorderPressedProperty = DependencyProperty.Register("ButtonBorderPressed", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush ButtonBorderPressed
        {
            get { return (Brush)GetValue(ButtonBorderPressedProperty); }
            set { SetValue(ButtonBorderPressedProperty, value); }
        }

        public static readonly DependencyProperty ButtonDefaultProperty = DependencyProperty.Register("ButtonDefault", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush ButtonDefault
        {
            get { return (Brush)GetValue(ButtonDefaultProperty); }
            set { SetValue(ButtonDefaultProperty, value); }
        }

        public static readonly DependencyProperty ButtonDefaultTextProperty = DependencyProperty.Register("ButtonDefaultText", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush ButtonDefaultText
        {
            get { return (Brush)GetValue(ButtonDefaultTextProperty); }
            set { SetValue(ButtonDefaultTextProperty, value); }
        }

        public static readonly DependencyProperty ButtonDisabledProperty = DependencyProperty.Register("ButtonDisabled", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush ButtonDisabled
        {
            get { return (Brush)GetValue(ButtonDisabledProperty); }
            set { SetValue(ButtonDisabledProperty, value); }
        }

        public static readonly DependencyProperty ButtonDisabledTextProperty = DependencyProperty.Register("ButtonDisabledText", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush ButtonDisabledText
        {
            get { return (Brush)GetValue(ButtonDisabledTextProperty); }
            set { SetValue(ButtonDisabledTextProperty, value); }
        }

        public static readonly DependencyProperty ButtonFocusedProperty = DependencyProperty.Register("ButtonFocused", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush ButtonFocused
        {
            get { return (Brush)GetValue(ButtonFocusedProperty); }
            set { SetValue(ButtonFocusedProperty, value); }
        }

        public static readonly DependencyProperty ButtonFocusedTextProperty = DependencyProperty.Register("ButtonFocusedText", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush ButtonFocusedText
        {
            get { return (Brush)GetValue(ButtonFocusedTextProperty); }
            set { SetValue(ButtonFocusedTextProperty, value); }
        }

        public static readonly DependencyProperty ButtonHoverProperty = DependencyProperty.Register("ButtonHover", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush ButtonHover
        {
            get { return (Brush)GetValue(ButtonHoverProperty); }
            set { SetValue(ButtonHoverProperty, value); }
        }

        public static readonly DependencyProperty ButtonHoverTextProperty = DependencyProperty.Register("ButtonHoverText", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush ButtonHoverText
        {
            get { return (Brush)GetValue(ButtonHoverTextProperty); }
            set { SetValue(ButtonHoverTextProperty, value); }
        }

        public static readonly DependencyProperty ButtonPressedProperty = DependencyProperty.Register("ButtonPressed", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush ButtonPressed
        {
            get { return (Brush)GetValue(ButtonPressedProperty); }
            set { SetValue(ButtonPressedProperty, value); }
        }

        public static readonly DependencyProperty ButtonPressedTextProperty = DependencyProperty.Register("ButtonPressedText", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush ButtonPressedText
        {
            get { return (Brush)GetValue(ButtonPressedTextProperty); }
            set { SetValue(ButtonPressedTextProperty, value); }
        }

        public static readonly DependencyProperty ButtonTextProperty = DependencyProperty.Register("ButtonText", typeof(Brush), typeof(UIStylesService), new PropertyMetadata(null));

        public Brush ButtonText
        {
            get { return (Brush)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }
    }
}
