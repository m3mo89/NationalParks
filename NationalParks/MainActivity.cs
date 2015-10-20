using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

//using NationalParks.Data;

namespace NationalParks.Droid
{
    [Activity(Label = "NationalParks.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        NationalParksAdapter _adapter;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            NationalParksData.Instance.DataDir = System.Environment.GetFolderPath(
                System.Environment.SpecialFolder.MyDocuments);
            NationalParksData.Instance.Load();

            _adapter = new NationalParksAdapter(this);

            FindViewById<ListView>(Resource.Id.parkListView).Adapter = _adapter;
            FindViewById<ListView>(Resource.Id.parkListView).ItemClick += ParkClicked;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.MainMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.actionNew:
                    StartActivity(typeof(EditActivity));
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        public void ParkClicked(object sender, ListView.ItemClickEventArgs e)
        {
            Intent intent = new Intent(this, typeof(DetailActivity));
            intent.PutExtra("parkid", _adapter[e.Position].Id);
            StartActivity(intent);
        }

        protected override void OnResume()
        {
            base.OnResume();
            _adapter.NotifyDataSetChanged();
        }
    }
}

