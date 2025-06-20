using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Threading;
using The_Fallen_Snow_WPF.classes;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System;
using Microsoft.VisualBasic;
using static PixelGame.GameWindow;

namespace PixelGame
{
    public class GameWindow : Window
    {
        public Boolean onBoard = true;
        public Canvas canvas = new Canvas
        {
            Width = 320,
            Height = 180,
            Background = Brushes.Black
        };
        public Viewbox viewbox = new Viewbox
        {
            Stretch = Stretch.Uniform
        };
        public The_Fallen_Snow_WPF.classes.Sound bg_sound;
        public Boolean LocationChoosed = false;

        private Canvas coordinatePanel;
        private TextBlock coordinateTextX;
        private TextBlock coordinateTextY;
        private bool coordinatePanelVisible = true;
        private Image _logoIcon;
        private Image _logoName;
        private Ellipse _glowEffect;
        private DispatcherTimer _opacityTimer;
        private DispatcherTimer _glowTimer;
        private DispatcherTimer _logoGrowthTimer;

        private const double LogoWidth = 64;
        private const double LogoHeight = 64;
        private const string LogoIconSource = "src/Images/logo_icon.png";
        private const string LogoNameSource = "src/Images/logo_name.png";
        private const double LogoInitialOpacity = 0;
        private const double LogoFadeSpeed = 0.05;
        private const double GlowWidth = 40;
        private const double GlowHeight = 40;
        private const double GlowInitialOpacity = 0.05;
        private const double GlowMaxOpacity = 0.5;
        private const double GlowFadeSpeed = 0.01;
        private const double AnimationIntervalMs = 30;
        private const double GlowAnimationIntervalMs = 50;
        private const double LogoGrowthIntervalMs = 30;
        private const double LogoGrowthScaleSpeed = 0.05;
        private const double LogoGrowthFadeSpeed = 0.05;
        private const double MainMenuFadeOutDurationMs = 1200;


        private Sound _bgSound;
        private Image _mainMenuBg;
        private Image _playButtonImage;
        private Image _playLabelButton;
        private Image _settingsButtonImage;
        private Image _settingsLabelButton;
        private Image _exitButtonImage;
        private Image _exitLabelButton;

        private const double MainMenuCanvasWidth = 320;
        private const double MainMenuCanvasHeight = 180;
        private const string BackgroundSoundPath = "src/Audio/Background/white_loneliness.mp3";
        private const string MainMenuThemePath = "src/Themes/main_menu_theme.png";
        private const double ButtonWidth = 98;
        private const double ButtonHeight = 26;
        private const double ButtonHorizontalCenterOffset = -1;
        private const double PlayButtonTopOffset = (180 - ButtonHeight * 3 - 32);
        private const double SettingsButtonTopOffset = (180 - ButtonHeight * 2 - 24);
        private const double ExitButtonTopOffset = (180 - ButtonHeight - 16);
        private const string ButtonImagePath = "src/Elements/button.png";
        private const string PlayLabelPath = "src/Elements/Labels/play";
        private const string SettingsLabelPath = "src/Elements/Labels/settings";
        private const string ExitLabelPath = "src/Elements/Labels/exit";
        private const double PlayOrSaveTransitionDurationMs = 300;
        private const double ExitGameTransitionDurationMs = 450;


        private Image _playOrLoadPanel;
        private Image _closeButtonPlayOrLoad;
        private Image _buttonPlayImage;
        private Image _buttonPlayLabel;
        private Image _buttonLoadImage;
        private Image _buttonLoadLabel;

        private const string PlayOrLoadPanelPath = "src/Themes/play_or_load_panel.png";
        private const double PanelWidth = 320;
        private const double PanelHeight = 180;

        private const string CloseButtonPath = "src/Elements/no_button.png";
        private const double CloseButtonWidth = 11;
        private const double CloseButtonHeight = 11;
        private const double CloseButtonRightOffset = 50;
        private const double CloseButtonTopOffset = 15;
        private const double CloseButtonHorizontalPadding = 5;

        private const double ButtonCommonYOffset = 15;
        private const double ButtonCommonYPadding1 = 12;
        private const double ButtonCommonYPadding2 = 20;

        private const double ButtonWidthChoose = 98;
        private const double ButtonHeightChoose = 26;
        private const string ButtonBackgroundPath = "src/Elements/button.png";
        private const string PlayLabelChoosePath = "src/Elements/Labels/start";
        private const string LoadLabelChoosePath = "src/Elements/Labels/load";

        private const double PlayButtonCalcRefWidth = 360;
        private const double PlayButtonCalcOffset1 = 100;
        private const double PlayButtonCalcOffset2 = 98;
        private const double PlayButtonCalcOffset3 = 26;

        private const double LoadButtonCalcRefWidth = 360;
        private const double LoadButtonCalcOffset = 16;

        private const double PlayLoadButtonAnimationDurationMs = 450;


        private ScrollViewer _mapScrollViewer;
        private Image _mapScrollableImage;

        private const double MapCanvasWidth = 960;
        private const double MapCanvasHeight = 540;
        private const string MapImageSource = "src/Map/the_map.png";
        private const double InventoryOpenAnimationDurationMs = 450;
        private static readonly Color LocationGlowColor = Color.FromRgb(160, 160, 160);

        private const string HomeLocationSource = "src/Images/Locations/location_home.png";
        private const double HomeLocationWidth = 195;
        private const double HomeLocationHeight = 173;
        private const double HomeLocationLeft = 356;
        private const double HomeLocationTop = 222;
        private const string HomeLocationName = "HOME";

        private const string SmallForestLocationSource = "src/Images/Locations/location_small_forest.png";
        private const double SmallForestLocationWidth = 206;
        private const double SmallForestLocationHeight = 147;
        private const double SmallForestLocationLeft = 198;
        private const double SmallForestLocationTop = 68;
        private const string SmallForestLocationName = "SMALL_FOREST";

        private const string GreatForestLocationSource = "src/Images/Locations/location_great_forest.png";
        private const double GreatForestLocationWidth = 238;
        private const double GreatForestLocationHeight = 171;
        private const double GreatForestLocationLeft = 40;
        private const double GreatForestLocationTop = 166;
        private const string GreatForestLocationName = "GREAT_FOREST";

        private const string BunkerLocationSource = "src/Images/Locations/location_bunker.png";
        private const double BunkerLocationWidth = 163;
        private const double BunkerLocationHeight = 94;
        private const double BunkerLocationLeft = 82;
        private const double BunkerLocationTop = 412;
        private const string BunkerLocationName = "BUNKER";

        private const string BearLocationSource = "src/Images/Locations/location_bear.png";
        private const double BearLocationWidth = 230;
        private const double BearLocationHeight = 115;
        private const double BearLocationLeft = 645;
        private const double BearLocationTop = 46;
        private const string BearLocationName = "BEAR";

        private const string RobbersLocationSource = "src/Images/Locations/location_robbers.png";
        private const double RobbersLocationWidth = 197;
        private const double RobbersLocationHeight = 146;
        private const double RobbersLocationLeft = 714;
        private const double RobbersLocationTop = 195;
        private const string RobbersLocationName = "ROBBERS";

        private const string FinalBossLocationSource = "src/Images/Locations/final_boss.png";
        private const double FinalBossLocationWidth = 400;
        private const double FinalBossLocationHeight = 200;
        private const double FinalBossLocationLeft = 540;
        private const double FinalBossLocationTop = 342;
        private const string FinalBossLocationName = "FINAL_BOSS";

        private const double DefaultFadeDurationMs = 1200;
        private static readonly Brush FadeColor = Brushes.Black;

        private Canvas _currentMapCanvas;
        private Canvas _currentMainCanvas;

        private const double GameCanvasWidth = 640;
        private const double GameCanvasHeight = 360;
        private const double MapContainerWidth = 2048;
        private const double MapContainerHeight = 2048;
        private const double LoadLocationFadeDurationMs = 1200;
        private const int MaxInstrumentSlots = 5;


        private Rectangle _locationInfoBlackout;
        private Image _locationInfoBackPanel;
        private Image _locationInfoCloseButton;
        private Image _locationInfoLoadLocationButton;
        private Image _locationInfoLoadLocationLabel;

        private const double LocationInfoBlackoutOpacity = 0.15;
        private static readonly Brush LocationInfoBlackoutColor = Brushes.Black;

        private const double InfoPanelWidth = 750;
        private const double InfoPanelHeight = 370;
        private const string InfoPanelSource = "src/Elements/back_panel.png";
        private const double InfoPanelLeftOffsetCalcRefWidth = 960;
        private const double InfoPanelTopOffsetCalcRefHeight = 540;


        private const double CloseButtonWidthInfo = 33;
        private const double CloseButtonHeightInfo = 33;
        private const string CloseButtonSourceInfo = "src/Elements/big_close_button.png";
        private const double CloseButtonRightOffset1 = 705;
        private const double CloseButtonTopAbsolute = 95;

        private const double LoadButtonWidthInfo = 288;
        private const double LoadButtonHeightInfo = 78;
        private const string LoadButtonSourceInfo = "src/Elements/big_button.png";
        private const string LoadLabelSourceInfo = "src/Elements/Labels/open";
        private const double LoadButtonLeftAbsolute = 130;
        private const double LoadButtonTopOffsetPanel = 270;

        private const double LoadLocationTransitionDurationMs = 450;

        private DispatcherTimer _gameTimer1 = null;
        private Double _mapOffsetX = InitialMapOffsetX;
        private Double _mapOffsetY = InitialMapOffsetY;
        private Double _playerMoveSpeed = PlayerMoveSpeed;
        private Image _playerImage;

        private bool _moveUp = false;
        private bool _moveDown = false;
        private bool _moveLeft = false;
        private bool _moveRight = false;

        private DispatcherTimer _coordinateUpdateTimer;

        private const double PlayerWidth = 32;
        private const double PlayerHeight = 64;
        private const string PlayerStaySource = "src/player/player_stay.png";
        private const double InitialMapOffsetX = -1024;
        private const double InitialMapOffsetY = -1024;
        private const double PlayerMoveSpeed = 3;
        private const double GameTimerIntervalMs = 16;
        private const double CoordinateUpdateIntervalSeconds = 0.75;
        private const double WeaponShootOriginX = 320;
        private const double WeaponShootOriginY = 180;
        private const int WeaponSlashWidthMelee = 10;
        private const int WeaponSlashWidthOther = 24;
        private const int WeaponSlashHeightMelee = 10;
        private const int WeaponSlashHeightOther = 24;

        private const double DefaultButtonWidth = 98;
        private const double DefaultButtonHeight = 26;
        private const string ButtonNormalSuffix = "_b.png";
        private const string ButtonHoverSuffix = "_w.png";
        private const string ButtonActiveSuffix = "_a.png";
        private const string ButtonHoverSoundPath = "src/Audio/UI/buttonHover.mp3";
        private const string ButtonTouchSoundPath = "src/Audio/UI/buttonTouch.mp3";

        private Canvas _coordinatePanel;
        private TextBlock _coordinateTextX;
        private TextBlock _coordinateTextY;
        private bool _coordinatePanelVisible = true;


        private const double CoordinatePanelWidth = 130;
        private const double CoordinatePanelHeight = 50;
        private static readonly Brush CoordinatePanelTextColor = Brushes.Black;
        private const double CoordinatePanelTextFontSize = 12;
        private const double CoordinateTextXLeftOffset = 8;
        private const double CoordinateTextXTopOffset = 5;
        private const double CoordinateTextYLeftOffset = 8;
        private const double CoordinateTextYTopOffset = 22;
        private const double CoordinatePanelTopAbsolute = 10;
        private const double CoordinatePanelRightOffset = 10;

        private const BitmapScalingMode DefaultBitmapScalingMode = BitmapScalingMode.NearestNeighbor;


        private const double HealthPanelLeftOffset = 4;
        private const double HealthPanelTopOffset = 5;
        private const double FreezePanelRightOffset = 240;
        private const double FreezePanelWidthForOffset = 240;
        private const double FreezePanelTopOffset = 5;

        private const double IndicatorsUpdateIntervalMs = 100;
        private const double FreezingIntervalMs = 5000;
        private const double AntiFreezingIntervalMs = 1250;
        private const double SnowingIntervalMs = 15000;


        private Canvas HealthPanel = null;
        private Int32[] hearts = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        private Image[] Hearts;
        private Canvas FreezePanel = null;
        private Int32[] snowflakes = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        private Image[] Snowflakes;


        private DispatcherTimer Snowing;
        Boolean nearTheFire = false;

        private DispatcherTimer IndicatorsUpdate;
        private DispatcherTimer Freezing;
        private DispatcherTimer AntiFreezing;

        private const int HeartCount = 10;
        private const int FullHeartState = 1;
        private const double HeartImageWidth = 24;
        private const double HeartImageHeight = 24;
        private const double HeartImageXOffset = 24;
        private const double HeartImageYOffset = 0;
        private const string HeartImagePathPrefix = "src/player/Indicators/health_";
        private const double HealthRemoverIntervalMs = 600;

        private double _currentScale = 1.0;
        private double _currentFade = 1.0;

        private double _currentGlowOpacity = GlowInitialOpacity;
        private bool _fadingOutGlow = false;

        private Boolean isCanRemoveHealth = true;
        private DispatcherTimer HealthRemover;

        private Rectangle frozenForeGround = null;

        private Int32[] instruments = { 2, 3, 4, 5, 6 };
        private Image[] Instruments;
        private Rectangle Cursor = null;
        private Int32 cursor = 1;
        private Boolean isCanShoot = true;
        private Boolean isCanDamage = true;

        private const double ItemSlotWidth = 28;
        private const double ItemSlotHeight = 28;
        private const string ItemSlotImagePath = "src/Items/item_slot.png";
        private const string ItemImagePathPrefix = "src/Items/item";
        private const double InstrumentalPanelBaseWidth = 320;
        private const double InstrumentalPanelBaseHeight = 360;
        private const double InstrumentalPanelHorizontalMultiplier = 14;
        private const double InstrumentalPanelBottomOffset = 32;

        private const double CursorWidth = 26;
        private const double CursorHeight = 26;
        private static readonly Brush CursorFillColor = Brushes.White;
        private const double CursorOpacity = 0.225;
        private const double CursorTopOffset = 31;
        private const double CursorLeftAdjust = 1;

        private const double PlayerCollisionWidth = 32;
        private const double PlayerCollisionHeight = 64;
        public List<GameZone> allZones = new List<GameZone>();
        private DispatcherTimer FirePlaceZone = null;

        private DispatcherTimer workbenchTimer;
        private bool isInWorkbenchZone = false;
        private Canvas craftPanel;

        private Canvas statPanel = null;
        private TextBlock blueprintLabel = null;
        private Int32 countOfBlueprint = 0;
        private bool hasBlueprint = false;

        private bool hasSnowCannon = false;


        public GameWindow()
        {
            Width = SystemParameters.PrimaryScreenWidth;
            Height = SystemParameters.PrimaryScreenHeight;
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.NoResize;
            WindowState = WindowState.Maximized;
            Background = Brushes.Black;
            Topmost = true;

            viewbox.Child = canvas;
            Content = viewbox;

            InitializeLogoAnimation();
            InitializeGlowEffect();
        }
        private void InitializeLogoAnimation()
        {
            _logoIcon = new MyImage
            {
                Width = (int)LogoWidth,
                Height = (int)LogoHeight,
                Source = LogoIconSource,
                Left = (int)(canvas.Width - LogoWidth) / 2,
                Top = (int)(canvas.Height - LogoHeight) / 2
            }.Create();
            canvas.Children.Add(_logoIcon);

            _logoName = new MyImage
            {
                Width = 192,
                Height = 32,
                Source = LogoNameSource,
                Left = (int)(canvas.Width - 192) / 2,
                Top = (int)(canvas.Height - 32 - 16)
            }.Create();
            canvas.Children.Add(_logoName);

            _logoIcon.Opacity = LogoInitialOpacity;
            _logoName.Opacity = LogoInitialOpacity;

            _opacityTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(AnimationIntervalMs) };
            _opacityTimer.Tick += OnLogoOpacityTimerTick;
            _opacityTimer.Start();
        }
        private void OnLogoOpacityTimerTick(object sender, EventArgs e)
        {
            _logoIcon.Opacity += LogoFadeSpeed;
            _logoName.Opacity += LogoFadeSpeed;

            if (_logoIcon.Opacity >= 1)
            {
                _logoIcon.Opacity = 1;
                _logoName.Opacity = 1;
                _opacityTimer.Stop();
                _opacityTimer = null;
            }
        }
        private void InitializeGlowEffect()
        {
            _glowEffect = new Ellipse
            {
                Width = GlowWidth,
                Height = GlowHeight,
                IsHitTestVisible = false,
                Opacity = GlowInitialOpacity,
                Fill = new RadialGradientBrush(Colors.White, Colors.Transparent)
                {
                    GradientOrigin = new Point(0.5, 0.5),
                    Center = new Point(0.5, 0.5),
                    RadiusX = 0.5,
                    RadiusY = 0.5
                }
            };
            Canvas.SetLeft(_glowEffect, (canvas.Width - GlowWidth) / 2);
            Canvas.SetTop(_glowEffect, (canvas.Height - GlowHeight) / 2);
            canvas.Children.Insert(0, _glowEffect);

            _glowTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(GlowAnimationIntervalMs) };
            _glowTimer.Tick += OnGlowTimerTick;
            _glowTimer.Start();

            _logoIcon.MouseLeftButtonDown += OnLogoIconMouseLeftButtonDown;
        }
        private void OnGlowTimerTick(object sender, EventArgs e)
        {
            if (_fadingOutGlow)
            {
                _currentGlowOpacity -= GlowFadeSpeed;
                if (_currentGlowOpacity <= GlowInitialOpacity) _fadingOutGlow = false;
            }
            else
            {
                _currentGlowOpacity += GlowFadeSpeed;
                if (_currentGlowOpacity >= GlowMaxOpacity) _fadingOutGlow = true;
            }
            _glowEffect.Opacity = _currentGlowOpacity;
        }
        private void OnLogoIconMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _logoGrowthTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(LogoGrowthIntervalMs) };
            _logoGrowthTimer.Tick += OnLogoGrowthTimerTick;
            _logoGrowthTimer.Start();
        }
        private void OnLogoGrowthTimerTick(object sender, EventArgs e)
        {
            _currentScale += LogoGrowthScaleSpeed;
            _currentFade -= LogoGrowthFadeSpeed;

            _logoIcon.RenderTransform = new ScaleTransform(_currentScale, _currentScale, _logoIcon.Width / 2, _logoIcon.Height / 2);
            _glowEffect.RenderTransform = new ScaleTransform(_currentScale, _currentScale, _glowEffect.Width / 2, _glowEffect.Height / 2);
            _logoName.RenderTransform = new ScaleTransform(_currentScale, _currentScale, _logoName.Width / 2, _logoName.Height / 2);

            _logoIcon.Opacity = _currentFade;
            _glowEffect.Opacity = _currentFade;
            _logoName.Opacity = _currentFade;

            if (_currentFade <= 0)
            {
                _logoGrowthTimer.Stop();
                _logoGrowthTimer = null; 
                _glowTimer.Stop();
                _glowTimer = null;

                canvas.Children.Remove(_logoIcon);
                canvas.Children.Remove(_glowEffect);
                canvas.Children.Remove(_logoName);

                MainMenu();
                KeyboardHelper.BindKeyAction(canvas, Key.Escape, ModifierKeys.None, OpenSettings);
                FadeOut(MainMenuFadeOutDurationMs);
            }
        }
        public void MainMenu()
        {
            canvas.Children.Clear();
            canvas.Width = MainMenuCanvasWidth;
            canvas.Height = MainMenuCanvasHeight;

            _bgSound = new Sound(BackgroundSoundPath, true);
            _bgSound.Play();

            _mainMenuBg = CreateImage(MainMenuThemePath, MainMenuCanvasWidth, MainMenuCanvasHeight, 0, 0);
            canvas.Children.Add(_mainMenuBg);

            double buttonLeft = (MainMenuCanvasWidth - ButtonWidth) / 2 + ButtonHorizontalCenterOffset;

            _playButtonImage = CreateImage(ButtonImagePath, ButtonWidth, ButtonHeight, buttonLeft, PlayButtonTopOffset);
            canvas.Children.Add(_playButtonImage);

            _playLabelButton = CreateButton(PlayLabelPath, true, buttonLeft, PlayButtonTopOffset, () =>
            {
                var growTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(PlayOrSaveTransitionDurationMs) };
                growTimer.Tick += (s, e) =>
                {
                    ChoosePlayOrSave();
                    growTimer.Stop();
                };
                growTimer.Start();
            });
            canvas.Children.Add(_playLabelButton);

            _settingsButtonImage = CreateImage(ButtonImagePath, ButtonWidth, ButtonHeight, buttonLeft, SettingsButtonTopOffset);
            canvas.Children.Add(_settingsButtonImage);

            _settingsLabelButton = CreateButton(SettingsLabelPath, false, buttonLeft, SettingsButtonTopOffset, () =>
            {
                OpenSettings();
            });
            canvas.Children.Add(_settingsLabelButton);

            _exitButtonImage = CreateImage(ButtonImagePath, ButtonWidth, ButtonHeight, buttonLeft, ExitButtonTopOffset);
            canvas.Children.Add(_exitButtonImage);

            _exitLabelButton = CreateButton(ExitLabelPath, true, buttonLeft, ExitButtonTopOffset, () =>
            {
                var growTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(ExitGameTransitionDurationMs) };
                growTimer.Tick += (s, e) =>
                {
                    ExitGame();
                    growTimer.Stop();
                };
                growTimer.Start();
            });
            canvas.Children.Add(_exitLabelButton);
        }
        public void OpenSettings()
        {
            FadeIn(1000, () =>
            {
                ClearGame();
                MainMenu();
                FadeOut(1000);
            });
        }
        public void ChoosePlayOrSave()
        {
            _playOrLoadPanel = CreateImage(PlayOrLoadPanelPath, PanelWidth, PanelHeight, 0, 0);
            canvas.Children.Add(_playOrLoadPanel);

            _closeButtonPlayOrLoad = CreateImage(CloseButtonPath, CloseButtonWidth, CloseButtonHeight,
                PanelWidth - CloseButtonRightOffset - CloseButtonHorizontalPadding, CloseButtonTopOffset + ButtonCommonYPadding1);
            canvas.Children.Add(_closeButtonPlayOrLoad);

            double buttonY = PanelHeight - ButtonCommonYOffset - ButtonCommonYPadding1 - ButtonCommonYPadding2;
            double buttonPlayX = ((PlayButtonCalcRefWidth - PlayButtonCalcOffset1) / 2) - PlayButtonCalcOffset2 + PlayButtonCalcOffset3; 
            double buttonLoadX = LoadButtonCalcRefWidth / 2 - LoadButtonCalcOffset; 

            _buttonPlayImage = CreateImage(ButtonBackgroundPath, ButtonWidthChoose, ButtonHeightChoose, buttonPlayX, buttonY);
            canvas.Children.Add(_buttonPlayImage);

            _buttonPlayLabel = CreateButton(PlayLabelChoosePath, true, buttonPlayX, buttonY, OnPlayButtonClicked);
            _buttonPlayLabel.Width = ButtonWidthChoose;
            _buttonPlayLabel.Height = ButtonHeightChoose;
            canvas.Children.Add(_buttonPlayLabel);

            _buttonLoadImage = CreateImage(ButtonBackgroundPath, ButtonWidthChoose, ButtonHeightChoose, buttonLoadX, buttonY);
            canvas.Children.Add(_buttonLoadImage);

            _buttonLoadLabel = CreateButton(LoadLabelChoosePath, true, buttonLoadX, buttonY, OnLoadButtonClicked);
            _buttonLoadLabel.Width = ButtonWidthChoose;
            _buttonLoadLabel.Height = ButtonHeightChoose;
            canvas.Children.Add(_buttonLoadLabel);

            _closeButtonPlayOrLoad.MouseLeftButtonUp += OnCloseChoosePlayOrSaveClicked;
        }
        private void OnPlayButtonClicked()
        {
            var growTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(PlayLoadButtonAnimationDurationMs) };
            growTimer.Tick += (s, e) =>
            {
                ClearGame();
                StartGame();
                RemoveSave();
                CreateSave();
                Save();
                growTimer.Stop();
            };
            growTimer.Start();
        }
        private void OnLoadButtonClicked()
        {
            var growTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(PlayLoadButtonAnimationDurationMs) };
            growTimer.Tick += (s, e) =>
            {
                DownloadSave();
                growTimer.Stop();
            };
            growTimer.Start();
        }
        private void OnCloseChoosePlayOrSaveClicked(object sender, MouseButtonEventArgs e)
        {
            canvas.Children.Remove(_closeButtonPlayOrLoad);
            canvas.Children.Remove(_buttonPlayLabel);
            canvas.Children.Remove(_buttonLoadLabel);
            canvas.Children.Remove(_buttonPlayImage);
            canvas.Children.Remove(_buttonLoadImage);
            canvas.Children.Remove(_playOrLoadPanel);
        }
        public void ClearGame()
        {
            Cursor = null;
            allZones.Clear();

            if(frozenForeGround != null)
            {
                frozenForeGround = null;
            }

            if (IndicatorsUpdate != null)
            {
                IndicatorsUpdate.Stop();
                IndicatorsUpdate = null;
            }
           
            if (Freezing != null)
            {
                Freezing.Stop();
                Freezing = null;
            }
            
            if (AntiFreezing != null)
            {
                AntiFreezing.Stop();
                AntiFreezing = null;
            }

            if (Snowing != null)
            {
                Snowing.Stop();
                Snowing = null;
            }

            FreezePanel = null;
            HealthPanel = null;
            canvas.Children.Clear();
        }
        public void StartGame()
        {
            if (statPanel != null)
            {
                countOfBlueprint = 0;
                hasBlueprint = false;
                UpdateStatPanel();
            }

            for (Int32 i = 0; i < HeartCount; i++)
            {
                hearts[i] = FullHeartState;
            }

                OpenMap();
            FadeOut(1200);
        }
        public void DownloadSave()
        {

        }
        public void CreateSave()
        {

        }
        public void RemoveSave()
        {

        }
        public void Save()
        {

        }
        public void OpenMap()
        {
            
            canvas.Children.Clear();
            canvas.Width = MapCanvasWidth;
            canvas.Height = MapCanvasHeight;

            if (_gameTimer1 != null)
            {
                _gameTimer1 = null;
            }
            FreezePanel = null;
            HealthPanel = null;

            _mapScrollViewer = new ScrollViewer
            {
                Width = MapCanvasWidth,
                Height = MapCanvasHeight,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto
            };

            _mapScrollableImage = new Image
            {
                Source = new BitmapImage(new Uri(MapImageSource, UriKind.Relative)),
                Width = MapCanvasWidth,
                Height = MapCanvasHeight
            };
            RenderOptions.SetBitmapScalingMode(_mapScrollableImage, BitmapScalingMode.NearestNeighbor);
            _mapScrollViewer.Content = _mapScrollableImage;

            canvas.Children.Add(_mapScrollViewer);
            Canvas.SetLeft(_mapScrollViewer, 0);
            Canvas.SetTop(_mapScrollViewer, 0);

            _mapScrollViewer.Focusable = true;
            _mapScrollViewer.Focus();

            _mapScrollViewer.MouseLeftButtonUp += OnMapScrollViewerMouseLeftButtonUp;

            AddLocationToMap(HomeLocationSource, HomeLocationWidth, HomeLocationHeight, HomeLocationLeft, HomeLocationTop, HomeLocationName);
            AddLocationToMap(SmallForestLocationSource, SmallForestLocationWidth, SmallForestLocationHeight, SmallForestLocationLeft, SmallForestLocationTop, SmallForestLocationName);
            AddLocationToMap(GreatForestLocationSource, GreatForestLocationWidth, GreatForestLocationHeight, GreatForestLocationLeft, GreatForestLocationTop, GreatForestLocationName);
            AddLocationToMap(BunkerLocationSource, BunkerLocationWidth, BunkerLocationHeight, BunkerLocationLeft, BunkerLocationTop, BunkerLocationName);
            AddLocationToMap(BearLocationSource, BearLocationWidth, BearLocationHeight, BearLocationLeft, BearLocationTop, BearLocationName);
            AddLocationToMap(RobbersLocationSource, RobbersLocationWidth, RobbersLocationHeight, RobbersLocationLeft, RobbersLocationTop, RobbersLocationName);

            if (hasSnowCannon)
            {
                AddLocationToMap(FinalBossLocationSource, FinalBossLocationWidth, FinalBossLocationHeight, FinalBossLocationLeft, FinalBossLocationTop, FinalBossLocationName);
            }
        }
        private void AddLocationToMap(string source, double width, double height, double left, double top, string name)
        {
            var glow = GlowEffect.CreateGlow(canvas, width, height, left, top, LocationGlowColor);
            var location = CreateImage(source, width, height, left, top);

            if (location == null)
            {
                Console.WriteLine($"Ошибка загрузки изображения: {source}");
                return;
            }

            canvas.Children.Add(location);

            location.MouseEnter += (s, e) => GlowEffect.StartPulsing(glow);
            location.MouseLeave += (s, e) => GlowEffect.StopPulsing(glow);
            location.MouseLeftButtonDown += (s, e) =>
            {
                if (onBoard)
                {
                    ShowLocationInformation(name);
                }
            };
        }
        private void OnMapScrollViewerMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var growTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(InventoryOpenAnimationDurationMs) };
            growTimer.Tick += (s, ea) =>
            {
                InventoryOpen();
                growTimer.Stop();
            };
            growTimer.Start();
        }
        void FadeIn(double durationMs = DefaultFadeDurationMs, Action? onComplete = null)
        {
            var blackout = new Rectangle
            {
                Width = canvas.Width,
                Height = canvas.Height,
                Fill = FadeColor,
                Opacity = 0,
                IsHitTestVisible = false
            };
            Canvas.SetLeft(blackout, 0);
            Canvas.SetTop(blackout, 0);
            canvas.Children.Add(blackout);

            var animation = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromMilliseconds(durationMs)));
            animation.Completed += (s, e) => onComplete?.Invoke();
            blackout.BeginAnimation(UIElement.OpacityProperty, animation);
        }
        void FadeOut(double durationMs = DefaultFadeDurationMs, Action? onComplete = null)
        {
            var blackout = new Rectangle
            {
                Width = canvas.Width,
                Height = canvas.Height,
                Fill = FadeColor,
                Opacity = 1,
                IsHitTestVisible = false
            };
            Canvas.SetLeft(blackout, 0);
            Canvas.SetTop(blackout, 0);
            canvas.Children.Add(blackout);

            var animation = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromMilliseconds(durationMs)));
            animation.Completed += (s, e) =>
            {
                canvas.Children.Remove(blackout);
                onComplete?.Invoke();
            };
            blackout.BeginAnimation(UIElement.OpacityProperty, animation);
        }
        public void LoadLocation(string LocationName)
        {
            canvas.Children.Clear();
            canvas.Width = GameCanvasWidth;
            canvas.Height = GameCanvasHeight;
            onBoard = true;

            KeyboardHelper.BindKeyAction(canvas, Key.Escape, ModifierKeys.None, OnEscapeKeyPressed);
            KeyboardHelper.BindKeyAction(canvas, Key.Tab, ModifierKeys.None, OnTabKeyPressed);
            KeyboardHelper.BindKeyAction(canvas, Key.E, ModifierKeys.None, OpenCraftPanel);

            _currentMapCanvas = new Canvas
            {
                Width = MapContainerWidth,
                Height = MapContainerHeight
            };

            
            
            GenerateLocation(LocationName, _currentMapCanvas);
            InitializeStatPanel();

            _currentMainCanvas = new Canvas
            {
                Width = canvas.Width,
                Height = canvas.Height,
                Background = Brushes.Black
            };
            _currentMainCanvas.Children.Add(_currentMapCanvas);
            canvas.Children.Add(_currentMainCanvas);
            canvas.Children.Add(statPanel);

            SpawnPlayer(_currentMapCanvas);
            InitializeCoordinatePanel();
            InitializeInsturumentalPanel();

            for (int i = 0; i < MaxInstrumentSlots; i++)
            {
                int slotIndex = i;
                KeyboardHelper.BindKeyAction(canvas, (Key)(Key.D1 + i), ModifierKeys.None, () => ChooseSlot(slotIndex));
            }

            InitializeIndicators();
        }
        private void OnEscapeKeyPressed()
        {
            OpenSettings();
        }
        private void OnTabKeyPressed()
        {
            FadeIn(LoadLocationFadeDurationMs, () =>
            {
                ClearGame();
                OpenMap();
                FadeOut(LoadLocationFadeDurationMs);
            });
        }
        private void GenerateLocation(string locationName, Canvas mapCanvas)
            {
            mapCanvas.Children.Clear();
            switch (locationName)
            {
                case "HOME":
                    GenerateHome(mapCanvas);
                    break;
                case "SMALL_FOREST":
                    GenerateSmallForest(mapCanvas);
                    break;
                case "GREAT_FOREST":
                    GenerateGreatForest(mapCanvas);
                    break;
                case "BUNKER":
                    GenerateBunker(mapCanvas);
                    break;
                case "BEAR":
                    GenerateBearCave(mapCanvas);
                    break;
                case "ROBBERS":
                    GenerateRobbersCamp(mapCanvas);
                    break;
                case "FINAL_BOSS":
                    if (hasSnowCannon)
                    {
                        GenerateFinalBoss(mapCanvas);
                    }
                    break;
                default:
                    Console.WriteLine($"Неизвестная локация: {locationName}");
                    break;
            }
        }
        public void ShowLocationInformation(String locationName)
        {
            onBoard = false;

            _locationInfoBlackout = new Rectangle
            {
                Width = canvas.Width,
                Height = canvas.Height,
                Fill = LocationInfoBlackoutColor,
                Opacity = LocationInfoBlackoutOpacity,
                IsHitTestVisible = false
            };
            Canvas.SetLeft(_locationInfoBlackout, 0);
            Canvas.SetTop(_locationInfoBlackout, 0);
            canvas.Children.Add(_locationInfoBlackout);

            double panelLeft = (InfoPanelLeftOffsetCalcRefWidth - InfoPanelWidth) / 2;
            double panelTop = (InfoPanelTopOffsetCalcRefHeight - InfoPanelHeight) / 2;
            _locationInfoBackPanel = new MyImage
            {
                Width = (int)InfoPanelWidth,
                Height = (int)InfoPanelHeight,
                Source = InfoPanelSource,
                Left = (int)panelLeft,
                Top = (int)panelTop
            }.Create();
            canvas.Children.Add(_locationInfoBackPanel);

            _locationInfoCloseButton = new MyImage
            {
                Width = (int)CloseButtonWidthInfo,
                Height = (int)CloseButtonHeightInfo,
                Source = CloseButtonSourceInfo,
                Left = (int)panelLeft + (int)CloseButtonRightOffset1,
                Top = (int)CloseButtonTopAbsolute
            }.Create();
            _locationInfoCloseButton.MouseLeftButtonDown += (s, e) => OnLocationInfoCloseButtonClicked(locationName);
            canvas.Children.Add(_locationInfoCloseButton);

            _locationInfoLoadLocationButton = new MyImage
            {
                Width = (int)LoadButtonWidthInfo,
                Height = (int)LoadButtonHeightInfo,
                Source = LoadButtonSourceInfo,
                Left = (int)LoadButtonLeftAbsolute,
                Top = (int)panelTop + (int)LoadButtonTopOffsetPanel
            }.Create();
            canvas.Children.Add(_locationInfoLoadLocationButton);

            _locationInfoLoadLocationLabel = CreateButton(LoadLabelSourceInfo, true, LoadButtonLeftAbsolute, panelTop + LoadButtonTopOffsetPanel, () => OnLoadLocationFromInfoPanelClicked(locationName));
            _locationInfoLoadLocationLabel.Width = LoadButtonWidthInfo;
            _locationInfoLoadLocationLabel.Height = LoadButtonHeightInfo;
            canvas.Children.Add(_locationInfoLoadLocationLabel);
        }
        private void OnLocationInfoCloseButtonClicked(string locationName)
        {
            canvas.Children.Remove(_locationInfoLoadLocationLabel);
            canvas.Children.Remove(_locationInfoCloseButton);
            canvas.Children.Remove(_locationInfoLoadLocationButton);
            canvas.Children.Remove(_locationInfoBackPanel);
            canvas.Children.Remove(_locationInfoBlackout);
            onBoard = true;
        }
        private void OnLoadLocationFromInfoPanelClicked(string locationName)
        {
            var growTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(LoadLocationTransitionDurationMs) };
            growTimer.Tick += (s, e) =>
            {
                FadeIn(LoadLocationFadeDurationMs, () =>
                {
                    ClearGame();
                    LoadLocation(locationName);
                    FadeOut(LoadLocationFadeDurationMs);
                });
                growTimer.Stop();
            };
            growTimer.Start();
        }
        public void InventoryOpen()
        {

        }
        public void SpawnPlayer(Canvas mapCanvas)
        {
            _playerImage = new MyImage
            {
                Width = (int)PlayerWidth,
                Height = (int)PlayerHeight,
                Source = PlayerStaySource,
            }.Create();
            RenderOptions.SetBitmapScalingMode(_playerImage, BitmapScalingMode.NearestNeighbor);

            double playerCenterX = canvas.Width / 2 - _playerImage.Width / 2;
            double playerCenterY = canvas.Height / 2 - _playerImage.Height / 2;

            Canvas.SetLeft(_playerImage, playerCenterX);
            Canvas.SetTop(_playerImage, playerCenterY);
            canvas.Children.Add(_playerImage);

            _mapOffsetX = InitialMapOffsetX;
            _mapOffsetY = InitialMapOffsetY;
            _playerMoveSpeed = PlayerMoveSpeed;

            KeyboardHelper.BindKeyAction(canvas, Key.W, ModifierKeys.None, () => _moveUp = true, () => _moveUp = false);
            KeyboardHelper.BindKeyAction(canvas, Key.S, ModifierKeys.None, () => _moveDown = true, () => _moveDown = false);
            KeyboardHelper.BindKeyAction(canvas, Key.A, ModifierKeys.None, () => _moveLeft = true, () => _moveLeft = false);
            KeyboardHelper.BindKeyAction(canvas, Key.D, ModifierKeys.None, () => _moveRight = true, () => _moveRight = false);

            _gameTimer1 = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(GameTimerIntervalMs)
            };
            _gameTimer1.Tick += (s, e) => OnGameTimerTick(mapCanvas);

            _coordinateUpdateTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(CoordinateUpdateIntervalSeconds)
            };
            _coordinateUpdateTimer.Tick += OnCoordinateUpdateTimerTick;
            _coordinateUpdateTimer.Start();

            _gameTimer1.Start();

            mapCanvas.MouseLeftButtonDown += OnMapCanvasMouseLeftButtonDown;
        }
        private void OnGameTimerTick(Canvas mapCanvas)
        {
            Double dx = 0, dy = 0;
            if (_moveUp) dy -= _playerMoveSpeed;
            if (_moveDown) dy += _playerMoveSpeed;
            if (_moveLeft) dx -= _playerMoveSpeed;
            if (_moveRight) dx += _playerMoveSpeed;

            Double tempOffsetX = _mapOffsetX;
            Double tempOffsetY = _mapOffsetY;

            CheckZoneCollisions(_mapOffsetX, _mapOffsetY, dx, dy, ref tempOffsetX, ref tempOffsetY, mapCanvas);

            Double minOffsetX = -(mapCanvas.Width - canvas.Width);
            Double maxOffsetX = 0;
            Double minOffsetY = -(mapCanvas.Height - canvas.Height);
            Double maxOffsetY = 0;

            _mapOffsetX = Math.Max(minOffsetX, Math.Min(maxOffsetX, tempOffsetX));
            _mapOffsetY = Math.Max(minOffsetY, Math.Min(maxOffsetY, tempOffsetY));

            Canvas.SetLeft(mapCanvas, _mapOffsetX);
            Canvas.SetTop(mapCanvas, _mapOffsetY);
        }
        private void OnCoordinateUpdateTimerTick(object sender, EventArgs e)
        {
            if (!coordinatePanelVisible)
            {
                coordinateTextX.Text = $"X: {_mapOffsetX:F0}";
                coordinateTextY.Text = $"Y: {_mapOffsetY:F0}";
            }
        }
        private void OnMapCanvasMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point pos = e.GetPosition(canvas);

            if (cursor == 3)
            {
                if (isCanShoot)
                {
                    ShootRedSquare(new Point(WeaponShootOriginX, WeaponShootOriginY), pos);
                    isCanShoot = false;
                }
            }
            else if (cursor == 0 || cursor == 1 || cursor == 2)
            {
                if (isCanDamage)
                {
                    String texturePath = $"src/objects/weapon_{cursor}.png";
                    SpawnWeaponSlash(new Point(WeaponShootOriginX, WeaponShootOriginY), pos,
                                     cursor == 0 ? WeaponSlashWidthMelee : WeaponSlashWidthOther,
                                     cursor == 1 ? WeaponSlashHeightMelee : WeaponSlashHeightOther, texturePath);
                    isCanDamage = false;
                }
            }
        }
        public Image CreateButton(string basePath, Boolean isCanActive, double x, double y, Action onClick)
        {
            var img = new MyImage
            {
                Width = (int)DefaultButtonWidth,
                Height = (int)DefaultButtonHeight,
                Source = basePath + ButtonNormalSuffix,
                Left = (int)x,
                Top = (int)y
            }.Create();

            img.MouseEnter += (s, e) => OnButtonMouseEnter(img, basePath);
            img.MouseLeave += (s, e) => OnButtonMouseLeave(img, basePath);
            img.MouseLeftButtonUp += (s, e) => OnButtonMouseLeftButtonUp(img, basePath, isCanActive, onClick);

            return img;
        }
        private void OnButtonMouseEnter(Image buttonImage, string basePath)
        {
            buttonImage.Source = new BitmapImage(new Uri(System.IO.Path.Combine(Directory.GetCurrentDirectory(), basePath + ButtonHoverSuffix)));
            new Sound(ButtonHoverSoundPath, false).Play();
        }
        private void OnButtonMouseLeave(Image buttonImage, string basePath)
        {
            buttonImage.Source = new BitmapImage(new Uri(System.IO.Path.Combine(Directory.GetCurrentDirectory(), basePath + ButtonNormalSuffix)));
        }
        private void OnButtonMouseLeftButtonUp(Image buttonImage, string basePath, Boolean isCanActive, Action onClick)
        {
            if (isCanActive)
            {
                buttonImage.Source = new BitmapImage(new Uri(System.IO.Path.Combine(Directory.GetCurrentDirectory(), basePath + ButtonActiveSuffix)));
                new Sound(ButtonTouchSoundPath, false).Play();
            }
            onClick?.Invoke();
        }
        private Image CreateImage(string source, double width, double height, double left, double top)
        {
            var img = new MyImage
            {
                Width = (int)width,
                Height = (int)height,
                Source = source,
                Left = (int)left,
                Top = (int)top
            }.Create();

            RenderOptions.SetBitmapScalingMode(img, DefaultBitmapScalingMode);
            return img;
        }
        public void ExitGame()
        {
            ClearGame();
            bg_sound?.Stop();
            Application.Current.Shutdown();
        }
        private void InitializeCoordinatePanel()
        {
            _coordinatePanel = new Canvas
            {
                Width = CoordinatePanelWidth,
                Height = CoordinatePanelHeight,
                Visibility = _coordinatePanelVisible ? Visibility.Hidden : Visibility.Visible
            };

            _coordinateTextX = new TextBlock
            {
                Foreground = CoordinatePanelTextColor,
                FontSize = CoordinatePanelTextFontSize,
                Text = "X: 0"
            };
            Canvas.SetLeft(_coordinateTextX, CoordinateTextXLeftOffset);
            Canvas.SetTop(_coordinateTextX, CoordinateTextXTopOffset);
            _coordinatePanel.Children.Add(_coordinateTextX);

            _coordinateTextY = new TextBlock
            {
                Foreground = CoordinatePanelTextColor,
                FontSize = CoordinatePanelTextFontSize,
                Text = "Y: 0"
            };
            Canvas.SetLeft(_coordinateTextY, CoordinateTextYLeftOffset);
            Canvas.SetTop(_coordinateTextY, CoordinateTextYTopOffset);
            _coordinatePanel.Children.Add(_coordinateTextY);

            Canvas.SetTop(_coordinatePanel, CoordinatePanelTopAbsolute);
            Canvas.SetLeft(_coordinatePanel, canvas.Width - _coordinatePanel.Width - CoordinatePanelRightOffset);
            canvas.Children.Add(_coordinatePanel);

            KeyboardHelper.BindKeyAction(canvas, Key.F6, ModifierKeys.None, OnToggleCoordinatePanelVisibility);
        }
        private void OnToggleCoordinatePanelVisibility()
        {
            _coordinatePanelVisible = !_coordinatePanelVisible;
            _coordinatePanel.Visibility = _coordinatePanelVisible ? Visibility.Hidden : Visibility.Visible;
        }
        private void InitializeIndicators()
        {
            InitializeHealthPanel();
            canvas.Children.Add(HealthPanel);
            Canvas.SetLeft(HealthPanel, HealthPanelLeftOffset);
            Canvas.SetTop(HealthPanel, HealthPanelTopOffset);

            InitializeFreezePanel();
            canvas.Children.Add(FreezePanel);
            Canvas.SetRight(FreezePanel, FreezePanelRightOffset);
            Canvas.SetTop(FreezePanel, FreezePanelTopOffset);

            IndicatorsUpdate = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(IndicatorsUpdateIntervalMs) };
            Freezing = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(FreezingIntervalMs) };
            AntiFreezing = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(AntiFreezingIntervalMs) };
            Snowing = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(SnowingIntervalMs) };

            IndicatorsUpdate.Tick += OnIndicatorsUpdateTick;
            Freezing.Tick += OnFreezingTick;
            AntiFreezing.Tick += OnAntiFreezingTick;
            Snowing.Tick += OnSnowingTick;

            IndicatorsUpdate.Start();
            Snowing.Start();

            KeyboardHelper.BindKeyAction(canvas, Key.F8, ModifierKeys.None, OnAddHealthKeyPressed);
            KeyboardHelper.BindKeyAction(canvas, Key.F9, ModifierKeys.None, OnRemoveHealthKeyPressed);
            KeyboardHelper.BindKeyAction(canvas, Key.F1, ModifierKeys.None, OnAddSnowHealthKeyPressed);
            KeyboardHelper.BindKeyAction(canvas, Key.F2, ModifierKeys.None, OnRemoveSnowHealthKeyPressed);
        }
        private void OnIndicatorsUpdateTick(object sender, EventArgs e)
        {
            UpdateHearts();

            bool allFrozenOrEmpty = hearts.All(hp => hp == 0 || hp == 2);
            if (allFrozenOrEmpty && !Freezing.IsEnabled)
            {
                Freezing.Start();
            }
            else if (!allFrozenOrEmpty && Freezing.IsEnabled)
            {
                Freezing.Stop();
            }

            bool allNormalOrEmpty = hearts.All(hp => hp == 0 || hp == 1);
            if (allNormalOrEmpty && !AntiFreezing.IsEnabled)
            {
                AntiFreezing.Start();
            }
            else if (!allNormalOrEmpty && AntiFreezing.IsEnabled)
            {
                AntiFreezing.Stop();
            }

            UpdateSnowflakes();

            if (hearts.All(hp => hp == 0))
            {
                PlayerDeath();
                IndicatorsUpdate.Stop();
                Freezing.Stop();
                AntiFreezing.Stop();
                Snowing.Stop();
            }
        }
        private void OnFreezingTick(object sender, EventArgs e)
        {
            bool shouldBeFreezing = hearts.All(hp => hp == 0 || hp == 2);

            if (!shouldBeFreezing)
            {
                Freezing.Stop();
            }
            else
            {
                if (snowflakes.All(sf => sf == 1))
                {
                    RemoveHealth();
                    RemoveHealth();
                }
                else
                {
                    AddSnowflake();
                }
            }
        }
        private void OnAntiFreezingTick(object sender, EventArgs e)
        {
            bool shouldBeAntiFreezing = snowflakes.All(sf => sf == 0);

            if (shouldBeAntiFreezing)
            {
                AntiFreezing.Stop();
            }
            else
            {
                RemoveSnowflake();
            }
        }
        private void OnSnowingTick(object sender, EventArgs e)
        {
            if (!nearTheFire)
            {
                AddSnowHealth();
            }
        }
        private void OnAddHealthKeyPressed() => AddHealth();
        private void OnRemoveHealthKeyPressed() => RemoveHealth();
        private void OnAddSnowHealthKeyPressed() => AddSnowHealth();
        private void OnRemoveSnowHealthKeyPressed() => RemoveSnowHealth();
        private void InitializeHealthPanel()
        {
            if (HealthPanel == null)
            {
                HealthPanel = new Canvas();
                Hearts = new Image[HeartCount];

                for (Int32 i = 0; i < HeartCount; i++)
                {

                    Image heart = CreateImage(
                        $"{HeartImagePathPrefix}{hearts[i]}.png",
                        HeartImageWidth,
                        HeartImageHeight,
                        i * HeartImageXOffset,
                        HeartImageYOffset
                    );
                    Hearts[i] = heart;
                    HealthPanel.Children.Add(heart);
                }

                UpdateHearts();

                HealthRemover = new DispatcherTimer
                {
                    Interval = TimeSpan.FromMilliseconds(HealthRemoverIntervalMs)
                };
                HealthRemover.Tick += OnHealthRemoverTick;
            }
        }
        private void OnHealthRemoverTick(object sender, EventArgs e)
        {
            isCanRemoveHealth = true;
            HealthRemover.Stop();
        }
        private void UpdateHearts()
        {
            for (Int32 i = 0; i < hearts.Length; i++)
            {
                Hearts[i].Source = new BitmapImage(new Uri($"src/player/Indicators/health_{hearts[i]}.png", UriKind.Relative));
            }
        }
        private void AddHealth()
        {
            for (Int32 i = 0; i < hearts.Length; i++)
            {
                if (hearts[i] == 0)
                {
                    hearts[i] = 1;
                    break;
                }
            }
        }
        private void RemoveHealth(Int32 count=0)
        {
            if(isCanRemoveHealth)
            {
                for (; count > 0; count--)
                {
                    for (Int32 i = hearts.Length - 1; i >= 0; i--)
                    {
                        if (hearts[i] != 0)
                        {
                            hearts[i] = 0;
                            isCanRemoveHealth = false;
                            HealthRemover.Start();
                            break;
                        }
                    }
                }
            }
            
        }
        private void AddSnowHealth()
        {
            for (Int32 i = 0; i < hearts.Length; i++)
            {
                if (hearts[i] == 1)
                {
                    hearts[i] = 2;
                    break;
                }
            }
        }
        private void RemoveSnowHealth()
        {
            for (Int32 i = hearts.Length - 1; i >= 0; i--)
            {
                if (hearts[i] == 2)
                {
                    hearts[i] = 1;
                    break;
                }
            }
        }
        private void InitializeFreezePanel()
        {
            if (FreezePanel == null)
            {
                FreezePanel = new Canvas();
                Snowflakes = new Image[10];
                for (Int32 i = 0; i < snowflakes.Length; i++)
                {
                    snowflakes[i] = 0;

                    Image snowflake = CreateImage($"src/player/Indicators/snowflake_{snowflakes[i]}.png", 24, 24, i * 24, 0);
                    Snowflakes[i] = snowflake;
                    FreezePanel.Children.Add(snowflake);
                }
                UpdateSnowflakes();
            }
        }
        private void UpdateSnowflakes()
        {
            for (Int32 i = 0; i < snowflakes.Length; i++)
            {
                Snowflakes[i].Source = new BitmapImage(new Uri($"src/player/Indicators/snowflake_{snowflakes[i]}.png", UriKind.Relative));
            }
        }
        private void AddSnowflake()
        {
            if(frozenForeGround == null)
            {
                frozenForeGround = new Rectangle
                {
                    Width = canvas.Width,
                    Height = canvas.Height,
                    Fill = Brushes.LightBlue,
                    Opacity = 0,
                    IsHitTestVisible = false
                };
                Canvas.SetLeft(frozenForeGround, 0);
                Canvas.SetTop(frozenForeGround, 0);
                canvas.Children.Add(frozenForeGround);

                var animation = new DoubleAnimation(0, 0.225, new Duration(TimeSpan.FromMilliseconds(1200)));
                animation.Completed += (s, e) => { };
                frozenForeGround.BeginAnimation(UIElement.OpacityProperty, animation);
            }
            for (Int32 i = 0; i < snowflakes.Length; i++)
            {
                if (snowflakes[i] == 0)
                {
                    snowflakes[i] = 1;
                    break;
                }
            }
        }
        private void RemoveSnowflake()
        {
            for (Int32 i = snowflakes.Length - 1; i >= 0; i--)
            {
                if (snowflakes[i] == 1)
                {
                    snowflakes[i] = 0;
                    break;
                }
            }
            if (snowflakes.Count(sf => sf == 2) == 0)
            {
                if (frozenForeGround != null)
                {
                    var animation = new DoubleAnimation(0.225, 0, new Duration(TimeSpan.FromMilliseconds(1200)));
                    animation.Completed += (s, e) =>
                    {
                        canvas.Children.Remove(frozenForeGround);
                    };
                    frozenForeGround.BeginAnimation(UIElement.OpacityProperty, animation);
                    frozenForeGround = null;
                }
            }

        }
        private void PlayerDeath()
        {
            FadeIn(5000, () => {
                ExitGame();
            });
        }
        private void InitializeInsturumentalPanel()
        {
            Instruments = new Image[instruments.Length];
            double initialLeftOffset = InstrumentalPanelBaseWidth - (InstrumentalPanelHorizontalMultiplier * instruments.Length);
            double topPosition = InstrumentalPanelBaseHeight - InstrumentalPanelBottomOffset;

            for (Int32 i = 0; i < instruments.Length; i++)
            {
                if (hasSnowCannon || i < 4)
                {
                    double currentLeft = initialLeftOffset + (i * ItemSlotWidth);

                    var img = CreateImage(ItemSlotImagePath, ItemSlotWidth, ItemSlotHeight, currentLeft, topPosition);
                    canvas.Children.Add(img);

                    Instruments[i] = CreateImage(
                        $"{ItemImagePathPrefix}{instruments[i]}.png",
                        ItemSlotWidth,
                        ItemSlotHeight,
                        currentLeft,
                        topPosition
                    );
                    canvas.Children.Add(Instruments[i]);
                }
            }
            InitializeCursor(cursor);
        }
        private void InitializeCursor(Int32 index)
        {
            if (Cursor == null)
            {
                Cursor = new Rectangle()
                {
                    Width = CursorWidth,
                    Height = CursorHeight,
                    Fill = CursorFillColor,
                    Opacity = CursorOpacity,
                    IsHitTestVisible = false
                };
                Canvas.SetTop(Cursor, InstrumentalPanelBaseHeight - CursorTopOffset);
                canvas.Children.Add(Cursor);
            }
            ChooseSlot(index);
        }
        private void ChooseSlot(Int32 index)
        {
            if (Cursor != null)
            {
                double initialLeftOffset = InstrumentalPanelBaseWidth - (InstrumentalPanelHorizontalMultiplier * instruments.Length);
                double newLeft = initialLeftOffset + (index * ItemSlotWidth) + CursorLeftAdjust;
                Canvas.SetLeft(Cursor, newLeft);
                cursor = index;
            }
        }
        void SpawnWeaponSlash(Point origin, Point target, Int32 width, Int32 height, String imgPath)
        {
            Vector direction = target - origin;
            direction.Normalize();
            double distance = 40;

                Image slash = new Image
                {
                    Width = width,
                    Height = height,
                    Source = new BitmapImage(new Uri(imgPath, UriKind.Relative)),
                    RenderTransformOrigin = new Point(0.5, 0.5)
                };
                RenderOptions.SetBitmapScalingMode(slash, BitmapScalingMode.NearestNeighbor);
            

            TranslateTransform move = new TranslateTransform(origin.X, origin.Y);
            RotateTransform rotate = new RotateTransform();
            TransformGroup transforms = new TransformGroup();
            transforms.Children.Add(rotate);
            transforms.Children.Add(move);
            slash.RenderTransform = transforms;

            double angle = Math.Atan2(direction.Y, direction.X) * 180 / Math.PI + 90;
            rotate.Angle = angle;

            canvas.Children.Add(slash);

            int totalFrames = 10;
            int currentFrame = 0;
            double movePerFrame = distance / totalFrames;
            bool returning = false;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(16);

            timer.Tick += (s, e) =>
            {
                if (!returning)
                {
                    move.X += direction.X * movePerFrame;
                    move.Y += direction.Y * movePerFrame;
                    currentFrame++;

                    if (currentFrame >= totalFrames)
                    {
                        returning = true;
                        currentFrame = 0;
                    }
                }
                else
                {
                    move.X -= direction.X * movePerFrame;
                    move.Y -= direction.Y * movePerFrame;
                    currentFrame++;

                    if (currentFrame >= totalFrames)
                    {
                        timer.Stop();
                        canvas.Children.Remove(slash);
                        isCanDamage = true;
                    }
                }
            };

            timer.Start();
        }
        private Polygon CreateTriangle(Point center, double size, Brush color)
        {
            return new Polygon
            {
                Fill = color,
                Points = new PointCollection
        {
            new Point(center.X, center.Y - size),
            new Point(center.X - size, center.Y + size),
            new Point(center.X + size, center.Y + size)
        }
            };
        }
        private Rectangle CreateBullet(double size, Brush color)
        {
            return new Rectangle
            {
                Width = size,
                Height = size,
                Fill = color
            };
        }
        private void SpawnWeaponTriangle(Point clickPosition)
        {
            Polygon weapon = CreateTriangle(clickPosition, 10, Brushes.Red);
            canvas.Children.Add(weapon);
        }
        private void ShootRedSquare(Point from, Point to)
        {
            Rectangle bullet = CreateBullet(5, Brushes.DarkRed);
            Canvas.SetLeft(bullet, from.X - 2.5);
            Canvas.SetTop(bullet, from.Y - 2.5);
            canvas.Children.Add(bullet);

            Vector dir = to - from;
            dir.Normalize();

            double speed = 4;
            DispatcherTimer moveTimer = new DispatcherTimer();
            moveTimer.Interval = TimeSpan.FromMilliseconds(32);

            int lifetimeMs = 0;

            moveTimer.Tick += (s, e) =>
            {
                lifetimeMs += 32;
                if (lifetimeMs >= 2000)
                {
                    canvas.Children.Remove(bullet);
                    moveTimer.Stop();
                    isCanShoot = true;
                    return;
                }

                double left = Canvas.GetLeft(bullet) + dir.X * speed;
                double top = Canvas.GetTop(bullet) + dir.Y * speed;

                Canvas.SetLeft(bullet, left);
                Canvas.SetTop(bullet, top);
            };

            moveTimer.Start();
        }
        public enum ZoneType
        {
            Safe,
            Wall_Tree,
            Wall_Stone,
            Workbench,
            Enemy,
            Collectible
        }
        public class GameZone
        {
            public Rect Bounds { get; set; }
            public ZoneType Type { get; set; }
            public Action OnEnter { get; set; }
            public int WallType { get; set; }
            public Image ZoneImage { get; set; } 
            public string Name { get; set; } 

            public GameZone(Rect bounds, ZoneType type, Action onEnter = null, int wallType = 0, Image zoneImage = null, string name = null)
            {
                Bounds = bounds;
                Type = type;
                OnEnter = onEnter;
                WallType = wallType;
                ZoneImage = zoneImage;
                Name = name; 
            }
        }
        public void CheckZoneCollisions(double currentMapOffsetX, double currentMapOffsetY, double proposedDx, double proposedDy, ref double newMapOffsetX, ref double newMapOffsetY, Canvas mapCanvas)
        {
            double playerWorldX = -currentMapOffsetX + (canvas.Width / 2 - PlayerCollisionWidth / 2);
            double playerWorldY = -currentMapOffsetY + (canvas.Height / 2 - PlayerCollisionHeight / 2);

            Rect futurePlayerRect = new Rect(playerWorldX + proposedDx, playerWorldY + proposedDy, PlayerCollisionWidth, PlayerCollisionHeight);

            newMapOffsetX = currentMapOffsetX - proposedDx;
            newMapOffsetY = currentMapOffsetY - proposedDy;

            bool isInSafeZoneThisTick = false;

            foreach (var zone in allZones.ToList())
            {
                if (zone.Bounds.IntersectsWith(futurePlayerRect))
                {
                    switch (zone.Type)
                    {
                        case ZoneType.Safe:
                            isInSafeZoneThisTick = true;
                            if (!nearTheFire)
                            {
                                zone.OnEnter?.Invoke();
                            }
                            break;
                        case ZoneType.Workbench:
                            zone.OnEnter?.Invoke();
                            break;
                        case ZoneType.Enemy:
                            zone.OnEnter?.Invoke();
                            RemoveHealth(1);
                            break;
                        case ZoneType.Wall_Tree:
                        case ZoneType.Wall_Stone:
                            newMapOffsetX = currentMapOffsetX;
                            newMapOffsetY = currentMapOffsetY;
                            break;
                        case ZoneType.Collectible:
                            zone.OnEnter?.Invoke();
                            if (zone.ZoneImage != null)
                            {
                                if (mapCanvas.Children.Contains(zone.ZoneImage))
                                {
                                    mapCanvas.Children.Remove(zone.ZoneImage);
                                }
                                else if (canvas.Children.Contains(zone.ZoneImage))
                                {
                                    canvas.Children.Remove(zone.ZoneImage);
                                }
                            }
                            allZones.Remove(zone);
                            break;
                    }
                }
            }

            if (!isInSafeZoneThisTick && nearTheFire)
            {
                if (FirePlaceZone != null && FirePlaceZone.IsEnabled)
                {
                    FirePlaceZone.Stop();
                    nearTheFire = false;
                }
            }
        }
        public void AddZone(Canvas mapCanvas, Rect bounds, ZoneType type, Action onEnter = null, int wallType = 0, string imageSource = null)
        {
            var zone = new GameZone(bounds, type, onEnter, wallType);
            allZones.Add(zone);

            if (!string.IsNullOrEmpty(imageSource))
            {
                var img = new Image
                {
                    Width = bounds.Width,
                    Height = bounds.Height,
                    Source = new BitmapImage(new Uri(imageSource, UriKind.Relative))
                };
                RenderOptions.SetBitmapScalingMode(img, BitmapScalingMode.NearestNeighbor);
                Canvas.SetLeft(img, bounds.X);
                Canvas.SetTop(img, bounds.Y);
                mapCanvas.Children.Add(img);
            }
            else
            {
                Brush fill = type switch
                {
                    ZoneType.Safe => new SolidColorBrush(Color.FromArgb(128, 0, 255, 0)),
                    ZoneType.Wall_Tree => new SolidColorBrush(Color.FromArgb(160, 100, 60, 20)),
                    ZoneType.Workbench => new SolidColorBrush(Color.FromArgb(128, 0, 100, 255)),
                    ZoneType.Enemy => new SolidColorBrush(Color.FromArgb(160, 255, 0, 0)),
                    _ => Brushes.Transparent
                };

                Rectangle visual = new Rectangle
                {
                    Width = bounds.Width,
                    Height = bounds.Height,
                    Fill = fill
                };

                Canvas.SetLeft(visual, bounds.X);
                Canvas.SetTop(visual, bounds.Y);
                mapCanvas.Children.Add(visual);
            }
        }
        void OnFirePlace()
        {
            if (!nearTheFire)
            {
                FirePlaceZone = new DispatcherTimer
                {
                    Interval = TimeSpan.FromMilliseconds(800)
                };
                FirePlaceZone.Tick += (s, e) =>
                {
                    if (hearts.Count(hp => hp == 2) > 0)
                    {
                        RemoveSnowHealth();
                    }
                    else if (hearts.Count(hp => hp == 2) == 0)
                    {
                        FirePlaceZone.Stop();
                    }
                };
                FirePlaceZone.Start();
                nearTheFire = true;
            }
        }
        void OpenGUI()
        {
            InitializeWorkbenchTimer();
        }
        private void GenerateHome(Canvas mapCanvas)
        {
            Image mapImage = new Image
            {
                Width = 2048,
                Height = 2048,
                Source = new BitmapImage(new Uri("src/Map/home_map.png", UriKind.Relative))
            };
            RenderOptions.SetBitmapScalingMode(mapImage, BitmapScalingMode.NearestNeighbor);
            mapCanvas.Children.Add(mapImage);

            AddZone(mapCanvas, new Rect(1000, 1000, 64, 64), ZoneType.Safe, OnFirePlace, 0, "src/objects/fireplace.png");

            AddZone(mapCanvas, new Rect(1100, 1100, 64, 32), ZoneType.Enemy, () => { RemoveHealth(1); }, 0, "src/objects/kaban.png");
            AddZone(mapCanvas, new Rect(900, 900, 64, 32), ZoneType.Enemy, () => { RemoveHealth(1); }, 0, "src/objects/kaban.png");

            AddZone(mapCanvas, new Rect(1200, 900, 32, 64), ZoneType.Wall_Tree, () => { }, 0, "src/objects/tree.png");
            AddZone(mapCanvas, new Rect(800, 1100, 32, 64), ZoneType.Wall_Tree, () => { }, 0, "src/objects/tree.png");
            AddZone(mapCanvas, new Rect(1100, 800, 32, 64), ZoneType.Wall_Tree, () => { }, 0, "src/objects/tree.png");
            AddZone(mapCanvas, new Rect(700, 1200, 32, 64), ZoneType.Wall_Tree, () => { }, 0, "src/objects/tree.png"); 
            AddZone(mapCanvas, new Rect(950, 1050, 32, 64), ZoneType.Wall_Tree, () => { }, 0, "src/objects/tree.png");
        }
        private void GenerateSmallForest(Canvas mapCanvas)
        {
            Image mapImage = new Image
            {
                Width = 2048,
                Height = 2048,
                Source = new BitmapImage(new Uri("src/Map/home_map.png", UriKind.Relative))
            };
            RenderOptions.SetBitmapScalingMode(mapImage, BitmapScalingMode.NearestNeighbor);
            mapCanvas.Children.Add(mapImage);

            Random random = new Random();
            int numberOfTrees = 15;

            for (int i = 0; i < numberOfTrees; i++)
            {
                double x = random.Next(100, 1900);
                double y = random.Next(100, 1900);

                AddZone(mapCanvas, new Rect(x, y, 32, 64), ZoneType.Wall_Tree, () => { }, 0, "src/objects/tree.png");
            }
        }
        private void GenerateGreatForest(Canvas mapCanvas)
        {
            Image mapImage = new Image
            {
                Width = 2048,
                Height = 2048,
                Source = new BitmapImage(new Uri("src/Map/home_map.png", UriKind.Relative)) // Замените на вашу текстуру травы
            };
            RenderOptions.SetBitmapScalingMode(mapImage, BitmapScalingMode.NearestNeighbor);
            mapCanvas.Children.Add(mapImage);

            Random random = new Random();
            int numberOfTrees = 25;

            for (int i = 0; i < numberOfTrees; i++)
            {
                double x = random.Next(100, 1900);
                double y = random.Next(100, 1900);

                AddZone(mapCanvas, new Rect(x, y, 32, 64), ZoneType.Wall_Tree, () => { }, 0, "src/objects/tree.png"); 
            }

            AddZone(mapCanvas, new Rect(1460, 480, 100, 100), ZoneType.Enemy, () => {
                RemoveHealth(2);
            }, 0, "src/objects/wolf.png");

            AddZone(mapCanvas, new Rect(1300, 500, 100, 100), ZoneType.Enemy, () => {
                RemoveHealth(2);
            }, 0, "src/objects/wolf.png");
        }
        private void GenerateBunker(Canvas mapCanvas)
        {
            Image mapImage = new Image
            {
                Width = 2048,
                Height = 2048,
                Source = new BitmapImage(new Uri("src/Map/home_map.png", UriKind.Relative))
            };
            RenderOptions.SetBitmapScalingMode(mapImage, BitmapScalingMode.NearestNeighbor);
            mapCanvas.Children.Add(mapImage);
        }
        private void GenerateRobbersCamp(Canvas mapCanvas)
        {
            Image mapImage = new Image
            {
                Width = 2048,
                Height = 2048,
                Source = new BitmapImage(new Uri("src/Map/home_map.png", UriKind.Relative))
            };
            RenderOptions.SetBitmapScalingMode(mapImage, BitmapScalingMode.NearestNeighbor);
            mapCanvas.Children.Add(mapImage);
            AddZone(mapCanvas, new Rect(1000, 1000, 48, 48), ZoneType.Workbench, OpenGUI, 0, "src/objects/workbench.png");

            AddZone(mapCanvas, new Rect(1024, 1000, 16, 16), ZoneType.Collectible, OnBlueprintPickup, 0, "src/objects/blueprint.png");
        }
        private void GenerateBearCave(Canvas mapCanvas)
        {
            Image mapImage = new Image
            {
                Width = 2048,
                Height = 2048,
                Source = new BitmapImage(new Uri("src/Map/home_map.png", UriKind.Relative))
            };
            RenderOptions.SetBitmapScalingMode(mapImage, BitmapScalingMode.NearestNeighbor);
            mapCanvas.Children.Add(mapImage);

            Random random = new Random();
            int numberOfRocks = 10;
            int numberOfTrees = 7; 

            for (int i = 0; i < numberOfRocks; i++)
            {
                double x = random.Next(100, 1900);
                double y = random.Next(100, 1900);

                AddZone(mapCanvas, new Rect(x, y, 64, 32), ZoneType.Wall_Tree, () => { }, 0, "src/objects/stone.png");
            }

            for (int i = 0; i < numberOfTrees; i++)
            {
                double x = random.Next(100, 1900);
                double y = random.Next(100, 1900);

                AddZone(mapCanvas, new Rect(x, y, 32, 64), ZoneType.Wall_Tree, () => { }, 0, "src/objects/tree.png");
            }

            AddZone(mapCanvas, new Rect(1200, 500, 100, 100), ZoneType.Enemy, () => {
                RemoveHealth(3);
            }, 0, "src/objects/bear.png");
        }
        private void GenerateFinalBoss(Canvas mapCanvas)
        {
            Image mapImage = new Image
            {
                Width = 2048,
                Height = 2048,
                Source = new BitmapImage(new Uri("src/Map/home_map.png", UriKind.Relative))
            };
            RenderOptions.SetBitmapScalingMode(mapImage, BitmapScalingMode.NearestNeighbor);
            mapCanvas.Children.Add(mapImage);

            AddZone(mapCanvas, new Rect(500, 500, 100, 100), ZoneType.Enemy, () => {
                for(int i = 0; i < hearts.Length; i++)
                {
                    hearts[i] = 0;
                }
                PlayerDeath();
            }, 0, "src/objects/boss.png");
        }
        private void InitializeStatPanel()
        {
            if (statPanel == null)
            {
                statPanel = new Canvas();
                statPanel.Width = 150;
                statPanel.Height = 30; 
                statPanel.Background = new SolidColorBrush(Color.FromArgb(128, 0, 0, 0)); 

                blueprintLabel = new TextBlock();
                blueprintLabel.Text = "Чертёж: " + countOfBlueprint;
                blueprintLabel.Foreground = Brushes.Black; 
                blueprintLabel.FontSize = 14; 

                Canvas.SetLeft(blueprintLabel, 5);
                Canvas.SetTop(blueprintLabel, 5);

                statPanel.Children.Add(blueprintLabel);

                Canvas.SetLeft(statPanel, 10);
                Canvas.SetTop(statPanel, 30);
            }
        }
        private void OnBlueprintPickup()
        {
            if (!hasBlueprint)
            {
                hasBlueprint = true;
                countOfBlueprint++;
                UpdateStatPanel();
            }
        }
        private void UpdateStatPanel()
        {
            blueprintLabel.Text = "Чертёж: " + countOfBlueprint;
        }
        private void CraftSnowCannon()
        {
            if (hasBlueprint && countOfBlueprint > 0 && true)
            {
                hasSnowCannon = true;
                countOfBlueprint--;
                UpdateStatPanel();
            }
        }
        private void InitializeWorkbenchTimer ()
        {
            workbenchTimer = new DispatcherTimer();
            workbenchTimer.Interval = TimeSpan.FromMilliseconds(500);
            workbenchTimer.Tick += WorkbenchTimer_Tick;
            workbenchTimer.Start();
        }
        private void WorkbenchTimer_Tick(object sender, EventArgs e)
        {

        }
        private void OpenCraftPanel()
        {
            if (craftPanel == null)
            {
                craftPanel = new Canvas();
                craftPanel.Width = 200;
                craftPanel.Height = 100;
                craftPanel.Background = new SolidColorBrush(Color.FromArgb(200, 50, 50, 50));

                Canvas.SetLeft(craftPanel, canvas.Width / 2 - craftPanel.Width / 2);
                Canvas.SetTop(craftPanel, canvas.Height / 2 - craftPanel.Height / 2);

                if (hasBlueprint)
                {
                    Button craftButton = new Button();
                    craftButton.Content = "Создать снежную пушку";
                    craftButton.Width = 150;
                    craftButton.Height = 30;
                    craftButton.Click += CraftButton_Click;

                    Canvas.SetLeft(craftButton, craftPanel.Width / 2 - craftButton.Width / 2);
                    Canvas.SetTop(craftButton, craftPanel.Height / 2 - craftButton.Height / 2);

                    craftPanel.Children.Add(craftButton);
                }
                else
                {
                    Button closeButton = new Button();
                    closeButton.Content = "X";
                    Canvas.SetTop(closeButton, 5);
                    Canvas.SetLeft(closeButton, 5);
                    craftPanel.Children.Add(closeButton);
                    closeButton.Click += CloseButton_Click;


                    TextBlock noBlueprintText = new TextBlock();
                    noBlueprintText.Text = "У вас нет чертежа!";
                    noBlueprintText.Foreground = Brushes.White;
                    noBlueprintText.TextAlignment = TextAlignment.Center;

                    Canvas.SetLeft(noBlueprintText, craftPanel.Width / 2 - noBlueprintText.Width / 2);
                    Canvas.SetTop(noBlueprintText, craftPanel.Height / 2 - noBlueprintText.Height / 2);

                    craftPanel.Children.Add(noBlueprintText);
                }

                canvas.Children.Add(craftPanel);
            }
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            CloseCraftPanel();
        }
        private void CloseCraftPanel()
        {
            if (craftPanel != null)
            {
                canvas.Children.Remove(craftPanel);
                craftPanel = null;

                CloseCraftPanel();
                OnCraftSnowCannonSuccess();
            }
        }
        private void CraftButton_Click(object sender, RoutedEventArgs e)
        {
            if (hasBlueprint &&  true)
            {
                hasSnowCannon = true;

                CloseCraftPanel();
                OnCraftSnowCannonSuccess();

                canvas.Focus();
                Keyboard.Focus(canvas);
            }
        }
        private void OnCraftSnowCannonSuccess()
        {

        }
        private void CreateSnowGun()
        {
        }
    }
}