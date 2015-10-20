
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

//using NationalParks.Data;

namespace NationalParks.Droid
{
    [Activity(Label = "DetailActivity")]
    public class DetailActivity : Activity
    {
        NationalPark _park;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Detail);
            if (Intent.HasExtra("parkid"))
            {
                string parkId = Intent.GetStringExtra("parkid");
                _park = NationalParksData.Instance.Parks.FirstOrDefault(x => x.Id == parkId);
            }
            else
            {
                _park = new NationalPark();
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            ParkToUI();
        }

        protected void ParkToUI()
        {
            FindViewById<TextView>(Resource.Id.nameTextView).Text = _park.Name;
            FindViewById<TextView>(Resource.Id.descriptionTextView).Text = _park.Description;
            FindViewById<TextView>(Resource.Id.stateTextView).Text = _park.State;
            FindViewById<TextView>(Resource.Id.countryTextView).Text = _park.Country;
            FindViewById<TextView>(Resource.Id.longitudeTextView).Text = _park.Longitude.ToString();
            FindViewById<TextView>(Resource.Id.latitudeTextView).Text = _park.Latitude.ToString();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.DetailMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if ((requestCode == 1) && (resultCode == Result.Ok))
            {
                if (data.GetBooleanExtra("parkdeleted", false))
                {
                    Finish();
                }
            }
            else
            {
                base.OnActivityResult(requestCode, resultCode, data);
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.actionEdit:
                    Intent editIntent = new Intent(this, typeof(EditActivity));
                    editIntent.PutExtra("parkid", _park.Id);
                    StartActivityForResult(editIntent, 1);
                    return true;
                case Resource.Id.actionPhotos:

                    Intent urlIntent = new Intent(Intent.ActionView);
                    urlIntent.SetData(Android.Net.Uri.Parse(
                            String.Format("http://www.bing.com/images/search?q={0}", _park.Name)));
                    StartActivity(urlIntent);
                    return true;

                case Resource.Id.actionDirections:

                    if (_park.Latitude.HasValue && _park.Longitude.HasValue)
                    {
                        Intent mapIntent = new Intent(Intent.ActionView,
                            Android.Net.Uri.Parse(
                            string.Format("geo:0,0?q={0},{1}&z=16 ({2})",
                                    _park.Latitude, _park.Longitude, _park.Name)));
                        StartActivity(mapIntent);
                    }
                    return true; // Navigate to the Detail View 

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

    }
}