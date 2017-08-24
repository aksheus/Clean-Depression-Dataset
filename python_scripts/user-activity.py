import matplotlib
import csv
import os
import sys
"""
USAGE : python user-activity.py path_to_csv_file"""

if __name__ == '__main__':

	subjects = []
	averages = []
	standard_deviations = []
	grand_mean,minimum,maximum ,grand_sd = None,None,None,None 

	if os.path.exists(sys.argv[1]):
		with open(sys.argv[1],'r') as csvfile:
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
		print 'Csv File Does not Exist'
		exit(1)

def plot_bargraph(values,categories):
	pass

def plot_boxplot(values,categories):
	pass 

