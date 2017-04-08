using System;
using Plugin.VersionTracking;
using Xamarin.Forms;

namespace VersionTrackingSample
{
	public class VersionsPage : ContentPage
	{
		public VersionsPage()
		{
			var versionTracker = CrossVersionTracking.Current;
			var grid = new Grid
			{
				ColumnDefinitions =
				{
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)}
				},
				RowDefinitions =
				{
					new RowDefinition { Height = new GridLength(1, GridUnitType.Auto)}, // First launch
					new RowDefinition { Height = new GridLength(1, GridUnitType.Auto)}, // Titles
					new RowDefinition { Height = new GridLength(1, GridUnitType.Auto)}, // Current
					new RowDefinition { Height = new GridLength(1, GridUnitType.Auto)}, // First
					new RowDefinition { Height = new GridLength(1, GridUnitType.Auto)},  // History titles
					new RowDefinition { Height = new GridLength(1, GridUnitType.Auto)}  // History
				}
			};

			Title = "Version tracking";

			// First launch
			var installsLabel = new Label { Style = HeaderStyle };
			installsLabel.Text = versionTracker.IsFirstLaunchEver ?
				"First launch ever!" :
				"Meh.";
			Grid.SetColumnSpan(installsLabel, 2);
			grid.Children.Add(installsLabel);

			// Titles
			var versionsTitleLabel = new Label { Style = HeaderStyle, Text = "Versions" };
			Grid.SetColumn(versionsTitleLabel, 0);
			Grid.SetRow(versionsTitleLabel, 1);
			grid.Children.Add(versionsTitleLabel);

			var buildsTitleLabel = new Label { Style = HeaderStyle, Text = "Builds",  };
			Grid.SetColumn(buildsTitleLabel, 1);
			Grid.SetRow(buildsTitleLabel, 1);
			grid.Children.Add(buildsTitleLabel);

			// Current 
			var labelCurrentVersion = new Label()
			{
				Text = $"Current {versionTracker.CurrentVersion}" + (versionTracker.IsFirstLaunchForVersion ? "(first launch)" : "")
			};
			Grid.SetColumn(labelCurrentVersion, 0);
			Grid.SetRow(labelCurrentVersion, 2);
			grid.Children.Add(labelCurrentVersion);

			var labelCurrentBuild = new Label()
			{
				Text = $"Current: {versionTracker.CurrentBuild}" + (versionTracker.IsFirstLaunchForBuild ? "(first launch)" : "")
			};
			Grid.SetColumn(labelCurrentBuild, 1);
			Grid.SetRow(labelCurrentBuild, 2);
			grid.Children.Add(labelCurrentBuild);

			// First 
			var labelFirstVersion = new Label()
			{
				Text = $"First: {versionTracker.FirstInstalledVersion}"
			};
			Grid.SetColumn(labelFirstVersion, 0);
			Grid.SetRow(labelFirstVersion, 3);
			grid.Children.Add(labelFirstVersion);

			var labelFirstBuild = new Label()
			{
				Text = $"First: {versionTracker.FirstInstalledBuild}" 
			};
			Grid.SetColumn(labelFirstBuild, 1);
			Grid.SetRow(labelFirstBuild, 3);
			grid.Children.Add(labelFirstBuild);

			// History titles
			var versionHistoryTitle = new Label { Text = "History" };
			Grid.SetColumn(versionHistoryTitle, 0);
			Grid.SetRow(versionHistoryTitle, 4);
			grid.Children.Add(versionHistoryTitle);

			var buildHistoryTitle = new Label { Text = "History" };
			Grid.SetColumn(buildHistoryTitle, 1);
			Grid.SetRow(buildHistoryTitle, 4);
			grid.Children.Add(buildHistoryTitle);

			// History
			var versionList = new ListView();
			versionList.ItemsSource = versionTracker.VersionHistory;
			Grid.SetColumn(versionList, 0);
			Grid.SetRow(versionList, 5);
			grid.Children.Add(versionList);

			var buildList = new ListView();
			buildList.ItemsSource = versionTracker.BuildHistory;
			Grid.SetColumn(buildList, 1);
			Grid.SetRow(buildList, 5);
			grid.Children.Add(buildList);

			Content = grid;
		}

		static Style HeaderStyle = new Style(typeof(Label))
		{
			Setters = {
				new Setter { Property = Label.FontAttributesProperty, Value = FontAttributes.Bold },
				new Setter { Property = Label.MarginProperty, Value = 5 },
				new Setter { Property = Label.FontSizeProperty, Value = Device.GetNamedSize(NamedSize.Large, typeof(Label)) },
				new Setter { Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.Center }
			}
		};

	}
}


