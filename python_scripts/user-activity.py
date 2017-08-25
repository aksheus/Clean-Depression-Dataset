import matplotlib
import csv
import os
import sys
"""
USAGE : python user-activity.py path_to_csv_file1 path_to_csvfile2 path_to_csv_file3 bar_graph_title boxplot_title"""

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
	pass

def plot_boxplot(values,categories):
	pass 

if __name__ == '__main__':

	subjects1,averages1,standard_deviations1,grand_mean1,minimum1,maximum1,grand_sd1 = get_stuff(sys.argv[1])
	subjects2,averages2,standard_deviations2,grand_mean2,minimum2,maximum2,grand_sd2 = get_stuff(sys.argv[2])
	subjects3,averages3,standard_deviations3,grand_mean3,minimum3,maximum3,grand_sd3 = get_stuff(sys.argv[3])






