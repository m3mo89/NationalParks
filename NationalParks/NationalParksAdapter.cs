using System;
using Android.Widget;
using Android.App;
using Android.Views;

//using NationalParks.Data;

namespace NationalParks.Droid
{
    public class NationalParksAdapter : BaseAdapter<NationalPark>
    {
        #region implemented abstract members of BaseAdapter

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Android.Views.View GetView(int position,
            Android.Views.View convertView, Android.Views.ViewGroup parent)
        {
            View view = convertView;

            if (view == null)
            {
                view = _context.LayoutInflater.Inflate(
                    Android.Resource.Layout.SimpleListItem1, null);
            }

            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text =
                NationalParksData.Instance.Parks[position].Name;

            return view;
        }

        public override int Count
        {
            get
            {
                return NationalParksData.Instance.Parks.Count;
            }
        }

        #endregion

        #region implemented abstract members of BaseAdapter

        public override NationalPark this[int index]
        {
            get
            {
                return NationalParksData.Instance.Parks[index];
            }
        }

        #endregion

        private Activity _context;
        public NationalParksAdapter(Activity context)
        {
            _context = context;
        }
    }
}
