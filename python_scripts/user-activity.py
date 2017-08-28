import matplotlib.pyplot as plt
import numpy as np
import matplotlib
import csv
import os
import sys
"""
USAGE : python user-activity.py path_to_csv_file1 path_to_csvfile2 path_to_csv_file3 which_data_set"""

def get_stuff(filename):
	subjects = []
	averages = []
	standard_deviations = []
	grand_mean,minimum,maximum ,grand_sd = None,None,None,None 

	if os.path.exists(filename):
		with open(filename,'r') as csvfile:
			reader = csv.reader(csvfile,delimiter=',')
			for row in list(reader)[1:]:
				if row[-1] !='':
					subjects.append(row[0])
					averages.append(float(row[1]))
					standard_deviations.append(float(row[2]))
				else:
					if row[0] == 'Grand Mean':
						grand_mean=float(row[1])
					elif row[0] == 'min':
						minimum = float(row[1])
					elif row[0] =='max':
						maximum = float(row[1])
					elif row[0] == 'Grand SD of Means ' :
						grand_sd = float(row[1])
	else:
		print 'Csv File Does not Exist : {0}'.format(filename)
		exit(1)
 
	return subjects,averages,standard_deviations,grand_mean,minimum,maximum,grand_sd


def plot_bargraph(title,values,categories):
	y_pos = np.arange(len(categories))
	plt.bar(left=y_pos,height=values,align='center',alpha = 0.5)
	plt.xticks(y_pos,categories)
	plt.ylabel('Grand Average of Posts for all Users')
	plt.title(title)
	plt.show()

def plot_boxplot(title,values,categories):
	# Create a figure instance
	fig = plt.figure(1, figsize=(9, 6))
	# Create an axes instance
	ax = fig.add_subplot(111)
	# Create the boxplot
	bp = ax.boxplot(values,patch_artist=True)
	ax.set_xticklabels(categories)
	ax.set_title(title)
	# Save the figure
	fig.savefig('box.png', bbox_inches='tight')
	## change outline color, fill color and linewidth of the boxes
	for box in bp['boxes']:
	    # change outline color
	    box.set( color='#7570b3', linewidth=2)
	    # change fill color
	    box.set( facecolor = '#1b9e77' )

	## change color and linewidth of the whiskers
	for whisker in bp['whiskers']:
	    whisker.set(color='#7570b3', linewidth=2)

	## change color and linewidth of the caps
	for cap in bp['caps']:
	    cap.set(color='#7570b3', linewidth=2)

	## change color and linewidth of the medians
	for median in bp['medians']:
	    median.set(color='#b2df8a', linewidth=2)

	## change the style of fliers and their fill
	for flier in bp['fliers']:
	    flier.set(marker='o', color='#e7298a', alpha=0.5)
	

if __name__ == '__main__':

	subjects1,averages1,standard_deviations1,grand_mean1,minimum1,maximum1,grand_sd1 = get_stuff(sys.argv[1])
	subjects2,averages2,standard_deviations2,grand_mean2,minimum2,maximum2,grand_sd2 = get_stuff(sys.argv[2])
	subjects3,averages3,standard_deviations3,grand_mean3,minimum3,maximum3,grand_sd3 = get_stuff(sys.argv[3])

	plot_bargraph('Comparison of User-Activity in '+sys.argv[4],[grand_mean1,grand_mean2,grand_mean3],('day','month','year'))
	plot_boxplot('Distribution of Average Posts For Each User in '+sys.argv[4],[averages1,averages2,averages3],['day','month','year'])









